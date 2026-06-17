namespace ETicaret.Application.Features.Baskets.DTOs
{
    public class BasketDto
    {
        public Guid BasketId { get; set; }
        public Guid UserId { get; set; }
        public List<BasketItemDto> Items { get; set; } = [];
        public decimal TotalPrice { get; set; }

    }
}
