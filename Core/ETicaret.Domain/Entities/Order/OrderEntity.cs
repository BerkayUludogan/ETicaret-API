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
        public string? CargoCompany { get; set; }
        public string? TrackingNumber { get; set; }
        public DateTime? ShippedDate { get; set; }
        public ICollection<OrderItemEntity> Items { get; set; } = [];
        public ICollection<OrderStatusHistoryEntity> StatusHistories { get; set; } = [];


    }
}
