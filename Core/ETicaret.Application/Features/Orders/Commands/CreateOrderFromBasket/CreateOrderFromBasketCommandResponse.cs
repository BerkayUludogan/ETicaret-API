namespace ETicaret.Application.Features.Orders.Commands.CreateOrderFromBasket
{
    public class CreateOrderFromBasketCommandResponse
    {
        public Guid OrderId { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
