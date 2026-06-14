namespace ETicaret.Application.Features.Products.DTOs
{
    public class ProductListDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Slug { get; set; }
        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }
        public decimal? DiscountPrice { get; set; }

        public int StockQuantity { get; set; }
        public required string SKU { get; set; }

        public bool IsActive { get; set; }
        public bool IsFeatured { get; set; }

        public Guid CategoryId { get; set; }
        public string? CategoryName { get; set; }
    }
}
