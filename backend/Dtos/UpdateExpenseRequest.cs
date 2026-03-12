namespace server.Dtos
{
    public class UpdateExpenseRequest
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public int CategoryId { get; set; }
    }
}
