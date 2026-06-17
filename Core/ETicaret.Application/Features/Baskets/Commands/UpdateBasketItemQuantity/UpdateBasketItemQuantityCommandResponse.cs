namespace ETicaret.Application.Features.Baskets.Commands.UpdateBasketItemQuantity
{
    public class UpdateBasketItemQuantityCommandResponse
    { 
        public Guid BasketItemId { get; set; }
        public int Quantity { get; set; }
    }
}
