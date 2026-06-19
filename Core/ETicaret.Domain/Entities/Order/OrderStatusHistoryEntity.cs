using ETicaret.Domain.Entities.Common;
using ETicaret.Domain.Entities.Identity;
using ETicaret.Domain.Enums;

namespace ETicaret.Domain.Entities.Order
{
    public class OrderStatusHistoryEntity : BaseEntity
    {
        public Guid OrderId { get; set; }
        public OrderEntity Order { get; set; } = default!; 
        public OrderStatus OldStatus { get; set; }
        public OrderStatus NewStatus { get; set; } 
        public Guid? ChangedByUserId { get; set; }
        public AppUserEntity? ChangedByUser { get; set; }
    }
}
