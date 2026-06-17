using ETicaret.Domain.Entities.Catalog;
using ETicaret.Domain.Entities.Common;

namespace ETicaret.Domain.Entities.Basket
{
    public class BasketItemEntity : BaseEntity
    {
        public Guid BasketId { get; set; }
        public BasketEntity Basket { get; set; } = default!;

        public Guid ProductId { get; set; }
        public ProductEntity Product { get; set; } = default!;

        public int Quantity { get; set; }
    }
}
