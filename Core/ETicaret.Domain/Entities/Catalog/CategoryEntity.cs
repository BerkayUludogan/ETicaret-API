using ETicaret.Domain.Entities.Common;

namespace ETicaret.Domain.Entities.Catalog
{
    public class CategoryEntity : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string? Description { get; set; }

        public bool IsActive { get; set; } = true;

        public Guid? ParentCategoryId { get; set; }
        public CategoryEntity? ParentCategory { get; set; }
        public ICollection<CategoryEntity> SubCategories { get; set; } = [];
        public ICollection<ProductEntity> Products { get; set; } = [];
    }
}
