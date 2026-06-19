using ETicaret.Application.Features.Orders.DTOs;
using MediatR;

namespace ETicaret.Application.Features.Orders.Queries.GetOrderStatusHistory
{
    public class GetOrderStatusHistoryQueryRequest : IRequest<List<OrderStatusHistoryDto>>
    {
        public Guid OrderId { get; set; }
    }
}
