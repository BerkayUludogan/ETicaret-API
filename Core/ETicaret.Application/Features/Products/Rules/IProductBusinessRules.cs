using ETicaret.Domain.Entities.Catalog;

namespace ETicaret.Application.Features.Products.Rules
{
    public interface IProductBusinessRules
    {
        Task<ProductEntity> ProductMustExist(Guid productId);
        Task ProductSlugMustBeUnique(string slug);
        Task ProductSkuMustBeUnique(string sku);
        Task ProductCategoryMustExist(Guid categoryId);
        Task DiscountPriceMustBeLessThanPrice(decimal price, decimal? discountPrice);

        Task ProductSlugMustBeUniqueForUpdate(Guid productId, string slug);
        Task ProductSkuMustBeUniqueForUpdate(Guid productId, string sku);

    }
}
