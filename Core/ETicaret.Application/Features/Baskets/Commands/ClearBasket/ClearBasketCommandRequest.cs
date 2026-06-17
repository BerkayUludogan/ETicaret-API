using MediatR;

namespace ETicaret.Application.Features.Baskets.Commands.ClearBasket
{
    public class ClearBasketCommandRequest : IRequest<ClearBasketCommandResponse>
    {
        public Guid UserId { get; set; }
    }
}
