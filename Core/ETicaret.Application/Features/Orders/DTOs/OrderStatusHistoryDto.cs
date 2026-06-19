namespace ETicaret.Application.Features.Orders.DTOs
{
    public class OrderStatusHistoryDto
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public string OldStatus { get; set; } = string.Empty;
        public string NewStatus { get; set; } = string.Empty;
        public Guid? ChangedByUserId { get; set; }
        public string? ChangedByUserName { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
