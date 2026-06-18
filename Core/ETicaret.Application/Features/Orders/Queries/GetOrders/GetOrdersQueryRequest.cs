using ETicaret.Application.Features.Orders.DTOs;
using MediatR;

namespace ETicaret.Application.Features.Orders.Queries.GetOrders
{
    public class GetOrdersQueryRequest : IRequest<List<OrderDto>>
    {
    }
}
