using ETicaret.Application.Common.Abstractions.Caching;
using ETicaret.Application.Common.Constants;
using MediatR;

namespace ETicaret.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommandRequest : IRequest<CreateProductCommandResponse>, IInvalidateCache
    {
        public required string Name { get; set; }
        public required string Slug { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public decimal? DiscountPrice { get; set; }
        public int StockQuantity { get; set; }
        public required string SKU { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsFeatured { get; set; }
        public Guid CategoryId { get; set; }

        public string InvalidateCacheKeyPrefix => CacheKeys.AllProducts.Key;
    }
}
