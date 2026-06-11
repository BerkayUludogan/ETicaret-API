using ETicaret.Domain.Entities.Common;

namespace ETicaret.Domain.Entities.Catalog
{
    public class ProductEntity : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string? Description { get; set; }

        public decimal Price { get; set; }
        public decimal? DiscountPrice { get; set; }

        public int StockQuantity { get; set; }
        public string SKU { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;
        public bool IsFeatured { get; set; }

        public Guid CategoryId { get; set; }
        public CategoryEntity Category { get; set; } = default!;
    }
}
