using ETicaret.Application.Features.Orders.DTOs;
using MediatR;

namespace ETicaret.Application.Features.Orders.Queries.GetMyOrders
{
    public class GetMyOrdersQueryRequest : IRequest<List<OrderDto>>
    {
        public Guid UserId { get; set; }
    }
}
