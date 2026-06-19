using ETicaret.Domain.Enums;

namespace ETicaret.Application.Features.Orders.Commands.CancelOrder
{
    public class CancelOrderCommandResponse
    {
        public Guid OrderId { get; set; }
        public OrderStatus Status { get; set; }
    }
}
