using Microsoft.EntityFrameworkCore;
using server.Data;
using server.Dtos;
using server.Models;
using server.Services.Interfaces;

namespace server.Services
{
    public class ExpenseService(AppDbContext context) : IExpense
    {
        public async Task<List<ExpenseResponse>> GetAllExpensesAsync(int userId)
        {
            return await context.Expenses
                .Where(e => e.UserId == userId)
                .Include(e => e.Category)
                .Select(e => new ExpenseResponse
                {
                    Id = e.Id,
                    Title = e.Title,
                    Description = e.Description,
                    Amount = e.Amount,
                    Date = e.Date,
                    CreatedAt = e.CreatedAt,
                    UserId = e.UserId,
                    CategoryId = e.CategoryId,
                    CategoryName = e.Category.Name
                })
                .ToListAsync();
        }

        public async Task<ExpenseResponse?> GetExpenseByIdAsync(int id)
        {
            var expense = await context.Expenses
                .Include(e => e.Category)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (expense is null) return null;

            return new ExpenseResponse
            {
                Id = expense.Id,
                Title = expense.Title,
                Description = expense.Description,
                Amount = expense.Amount,
                Date = expense.Date,
                CreatedAt = expense.CreatedAt,
                UserId = expense.UserId,
                CategoryId = expense.CategoryId,
                CategoryName = expense.Category.Name
            };
        }

        public async Task<ExpenseResponse> CreateExpenseAsync(CreateExpenseRequest expense)
        {
            var newExpense = new Expense
            {
                Title = expense.Title,
                Description = expense.Description,
                Amount = expense.Amount,
                Date = expense.Date,
                UserId = expense.UserId,
                CategoryId = expense.CategoryId
            };

            context.Expenses.Add(newExpense);
            await context.SaveChangesAsync();

            await context.Entry(newExpense).Reference(e => e.Category).LoadAsync();

            return new ExpenseResponse
            {
                Id = newExpense.Id,
                Title = newExpense.Title,
                Description = newExpense.Description,
                Amount = newExpense.Amount,
                Date = newExpense.Date,
                CreatedAt = newExpense.CreatedAt,
                UserId = newExpense.UserId,
                CategoryId = newExpense.CategoryId,
                CategoryName = newExpense.Category.Name
            };
        }

        public async Task<bool> UpdateExpenseAsync(int id, UpdateExpenseRequest expense)
        {
            var existing = await context.Expenses.FindAsync(id);
            if (existing is null) return false;

            existing.Title = expense.Title;
            existing.Description = expense.Description;
            existing.Amount = expense.Amount;
            existing.Date = expense.Date;
            existing.CategoryId = expense.CategoryId;

            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteExpenseAsync(int id)
        {
            var expense = await context.Expenses.FindAsync(id);
            if (expense is null) return false;

            context.Expenses.Remove(expense);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
