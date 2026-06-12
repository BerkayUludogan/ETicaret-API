namespace ETicaret.Application.Features.Categories.DTOs
{
    public class CategoryListDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Slug { get; set; }
        public string? Description { get; set; }
        public Guid? ParentCategoryId { get; set; }
        public bool IsActive { get; set; }
    }
}
