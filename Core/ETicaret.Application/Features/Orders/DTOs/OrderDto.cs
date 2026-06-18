namespace ETicaret.Application.Features.Orders.DTOs
{
    public class OrderDto
    {
        public Guid OrderId { get; set; }
        public Guid UserId { get; set; }
        public string Status { get; set; } = string.Empty;
        public decimal TotalPrice { get; set; }
        public string ShippingAddress { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public List<OrderItemDto> Items { get; set; } = [];
    }
}