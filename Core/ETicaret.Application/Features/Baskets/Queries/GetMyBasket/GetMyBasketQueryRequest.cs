using ETicaret.Application.Features.Baskets.DTOs;
using MediatR;

namespace ETicaret.Application.Features.Baskets.Queries.GetMyBasket
{
    public class GetMyBasketQueryRequest : IRequest<BasketDto>
    {
        public Guid UserId { get; set; }
    }
}
