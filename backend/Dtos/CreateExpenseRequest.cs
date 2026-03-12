namespace server.Dtos
{
    public class CreateExpenseRequest
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }
    }
}
