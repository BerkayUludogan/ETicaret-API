using ETicaret.Application.Features.Orders.DTOs;
using MediatR;

namespace ETicaret.Application.Features.Orders.Queries.GetOrderById
{
    public class GetOrderByIdQueryRequest : IRequest<OrderDto>
    {
        public Guid OrderId { get; set; }
        public Guid UserId { get; set; }
        public bool IsAdmin { get; set; }
    }
}
