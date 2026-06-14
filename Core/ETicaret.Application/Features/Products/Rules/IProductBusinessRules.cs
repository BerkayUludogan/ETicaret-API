namespace ETicaret.Application.Features.Products.Rules
{
    public interface IProductBusinessRules
    {
        Task ProductSlugMustBeUnique(string slug);
        Task ProductSkuMustBeUnique(string sku);
        Task ProductCategoryMustExist(Guid categoryId);
        Task DiscountPriceMustBeLessThanPrice(decimal price, decimal? discountPrice);
    }
}
