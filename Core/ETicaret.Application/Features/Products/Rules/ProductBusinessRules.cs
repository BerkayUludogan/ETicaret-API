using ETicaret.Application.Common.Abstractions.UnitOfWorks;
using ETicaret.Application.Common.Exceptions;
using ETicaret.Application.Common.Exceptions.Errors;
using ETicaret.Domain.Entities.Catalog;

namespace ETicaret.Application.Features.Products.Rules
{
    public class ProductBusinessRules : IProductBusinessRules
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductBusinessRules(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task ProductSlugMustBeUnique(string slug)
        {
            var exists = await _unitOfWork
                .GetReadRepository<ProductEntity>()
                .GetSingleAsync(x => x.Slug == slug && !x.IsDeleted, false);

            if (exists is not null)
                throw new BusinessRuleException(ProductErrors.SlugAlreadyExists);
        }
        public async Task ProductSkuMustBeUnique(string sku)
        {
            var exists = await _unitOfWork.GetReadRepository<ProductEntity>()
                .GetSingleAsync(x => x.SKU == sku && !x.IsDeleted, false);

            if (exists is not null)
                throw new BusinessRuleException(ProductErrors.SkuAlreadyExists);
        }
        public async Task ProductCategoryMustExist(Guid categoryId)
        {
            var category = await _unitOfWork.GetReadRepository<CategoryEntity>()
                .GetByIdAsync(categoryId, false);
            if (category is null || category.IsDeleted || !category.IsActive)
                throw new BusinessRuleException(ProductErrors.CategoryNotFound);
        }
        public Task DiscountPriceMustBeLessThanPrice(decimal price, decimal? discountPrice)
        {
            if (discountPrice.HasValue && discountPrice.Value >= price)
                throw new BusinessRuleException(ProductErrors.DiscountPriceMustBeLessThanPrice);
            return Task.CompletedTask;
        }
    }
}
