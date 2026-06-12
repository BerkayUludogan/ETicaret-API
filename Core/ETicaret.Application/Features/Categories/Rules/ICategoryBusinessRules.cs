namespace ETicaret.Application.Features.Categories.Rules
{
    public interface ICategoryBusinessRules
    {
        Task CategoryNameMustBeUnique(string categoryName);
        Task CategorySlugMustBeUnique(string categorySlug);
        Task ParentCategoryMustExistIfProvided(Guid? parentCategoryId);
    }
}
