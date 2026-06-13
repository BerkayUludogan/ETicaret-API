using ETicaret.Domain.Entities.Catalog;

namespace ETicaret.Application.Features.Categories.Rules
{
    public interface ICategoryBusinessRules
    {
        Task CategoryNameMustBeUnique(string categoryName);
        Task CategorySlugMustBeUnique(string categorySlug);
        Task ParentCategoryMustExistIfProvided(Guid? parentCategoryId);

        Task<CategoryEntity> CategoryMustExist(Guid categoryId);
        Task CategoryNameMustBeUniqueForUpdate(Guid categoryId, string categoryName);
        Task CategorySlugMustBeUniqueForUpdate(Guid categoryId, string categorySlug);
        Task CategoryMustNotBeParentOfItself(Guid categoryId, Guid? parentCategoryId);
    }
}
