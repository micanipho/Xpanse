using Microsoft.AspNetCore.Mvc;
using server.Dtos;
using server.Services.Interfaces;

namespace server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController(IExpense expenseService) : ControllerBase
    {
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<List<ExpenseResponse>>> GetExpensesByUser(int userId)
        {
            var expenses = await expenseService.GetAllExpensesAsync(userId);
            return Ok(expenses);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ExpenseResponse>> GetExpenseById(int id)
        {
            var expense = await expenseService.GetExpenseByIdAsync(id);
            if (expense is null) return NotFound();
            return Ok(expense);
        }

        [HttpPost]
        public async Task<ActionResult<ExpenseResponse>> CreateExpense(CreateExpenseRequest expense)
        {
            var created = await expenseService.CreateExpenseAsync(expense);
            return CreatedAtAction(nameof(GetExpenseById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExpense(int id, UpdateExpenseRequest expense)
        {
            var updated = await expenseService.UpdateExpenseAsync(id, expense);
            if (!updated) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpense(int id)
        {
            var deleted = await expenseService.DeleteExpenseAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
