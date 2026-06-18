using ETicaret.Domain.Enums;

namespace ETicaret.Application.Features.Orders.Commands.UpdateOrderStatus
{
    public class UpdateOrderStatusCommandResponse
    {
        public Guid OrderId { get; set; }
        public OrderStatus Status { get; set; }
    }
}
