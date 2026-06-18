using ETicaret.Domain.Entities.Catalog;
using ETicaret.Domain.Entities.Common;

namespace ETicaret.Domain.Entities.Order
{
    public class OrderItemEntity : BaseEntity
    {
        public Guid OrderId { get; set; }
        public OrderEntity Order { get; set; } = default!;

        public Guid ProductId { get; set; }
        public ProductEntity Product { get; set; } = default!;
        public string ProductName { get; set; } = string.Empty;
        public string ProductSku { get; set; } = string.Empty;

        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
