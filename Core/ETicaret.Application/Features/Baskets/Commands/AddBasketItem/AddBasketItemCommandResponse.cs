namespace ETicaret.Application.Features.Baskets.Commands.AddBasketItem
{
    public class AddBasketItemCommandResponse
    {
        public Guid BasketId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
