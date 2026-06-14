namespace ETicaret.Application.Common.Exceptions.Errors
{
    public class ProductErrors
    {
        public const string ProductNotFound = "Product.ProductNotFound";
        public const string SlugAlreadyExists = "Product.SlugAlreadyExists";
        public const string SkuAlreadyExists = "Product.SkuAlreadyExists";
        public const string CategoryNotFound = "Product.CategoryNotFound";
        public const string DiscountPriceMustBeLessThanPrice = "Product.DiscountPriceMustBeLessThanPrice";
    }
}
