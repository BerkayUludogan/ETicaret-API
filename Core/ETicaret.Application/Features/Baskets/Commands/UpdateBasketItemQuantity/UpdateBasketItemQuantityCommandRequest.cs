using MediatR;

namespace ETicaret.Application.Features.Baskets.Commands.UpdateBasketItemQuantity
{
    public class UpdateBasketItemQuantityCommandRequest : IRequest<UpdateBasketItemQuantityCommandResponse>
    {
        public Guid UserId { get; set; }
        public Guid BasketItemId { get; set; }
        public int Quantity { get; set; }
    }
}
