namespace ETicaret.Application.Common.Exceptions.Errors
{
    public class CategoryErrors
    {
        public const string NameAlreadyExists = "Category.NameAlreadyExists";
        public const string SlugAlreadyExists = "Category.SlugAlreadyExists";
        public const string ParentCategoryNotFound = "Category.ParentCategoryNotFound";
        public const string CategoryNotFound = "Category.CategoryNotFound";
        public const string CategoryCannotBeParentOfItself = "Category.CategoryCannotBeParentOfItself";
    }
}
