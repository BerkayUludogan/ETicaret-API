using MediatR;

namespace ETicaret.Application.Features.Baskets.Commands.RemoveBasketItem
{
    public class RemoveBasketItemCommandRequest : IRequest<RemoveBasketItemCommandResponse>
    {
        public Guid UserId { get; set; }
        public Guid BasketItemId { get; set; }
    }
}
