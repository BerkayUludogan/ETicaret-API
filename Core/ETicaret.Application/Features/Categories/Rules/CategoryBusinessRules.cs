using ETicaret.Application.Common.Abstractions.UnitOfWorks;
using ETicaret.Application.Common.Exceptions;
using ETicaret.Application.Common.Exceptions.Errors;
using ETicaret.Domain.Entities.Catalog;

namespace ETicaret.Application.Features.Categories.Rules
{
    public class CategoryBusinessRules : ICategoryBusinessRules
    {
        private readonly IUnitOfWork _unitofWork;
        public CategoryBusinessRules(IUnitOfWork unitOfWork)
        {
            _unitofWork = unitOfWork;
        }
        public async Task CategoryNameMustBeUnique(string categoryName)
        {
            var exists = await _unitofWork
                .GetReadRepository<CategoryEntity>()
                .GetSingleAsync(x => x.Name == categoryName && !x.IsDeleted, false);

            if (exists is not null)
                throw new BusinessRuleException(CategoryErrors.NameAlreadyExists);
        }

        public async Task CategorySlugMustBeUnique(string categorySlug)
        {
            var exists = await _unitofWork
                .GetReadRepository<CategoryEntity>()
                .GetSingleAsync(x => x.Slug == categorySlug && !x.IsDeleted, false);
            if (exists is not null)
                throw new BusinessRuleException(CategoryErrors.SlugAlreadyExists);
        }

        public async Task ParentCategoryMustExistIfProvided(Guid? parentCategoryId)
        {
            if (parentCategoryId is null)
                return;
            var parentCategory = await _unitofWork
                .GetReadRepository<CategoryEntity>()
                .GetByIdAsync(parentCategoryId.Value, false);

            if (parentCategory is null || parentCategory.IsDeleted)
                throw new BusinessRuleException(CategoryErrors.ParentCategoryNotFound);
        }
    }
}
