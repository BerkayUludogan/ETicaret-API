using ETicaret.Domain.Entities.Common;
using ETicaret.Domain.Entities.Identity;
using ETicaret.Domain.Enums;

namespace ETicaret.Domain.Entities.Order
{
    public class OrderEntity : BaseEntity
    {
        public Guid UserId { get; set; }
        public AppUserEntity User { get; set; } = default!;
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public decimal TotalPrice { get; set; }
        public string ShippingAddress { get; set; } = string.Empty;
        public ICollection<OrderItemEntity> Items { get; set; } = [];

    }
}
