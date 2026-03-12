using server.Dtos;

namespace server.Services.Interfaces
{
    public interface IExpense
    {
        Task<List<ExpenseResponse>> GetAllExpensesAsync(int userId);
        Task<ExpenseResponse?> GetExpenseByIdAsync(int id);
        Task<ExpenseResponse> CreateExpenseAsync(CreateExpenseRequest expense);
        Task<bool> UpdateExpenseAsync(int id, UpdateExpenseRequest expense);
        Task<bool> DeleteExpenseAsync(int id);
    }
}
