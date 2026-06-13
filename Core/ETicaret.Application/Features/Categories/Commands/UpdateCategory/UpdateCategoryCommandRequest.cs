using ETicaret.Application.Common.Abstractions.Caching;
using ETicaret.Application.Common.Constants;
using MediatR;

namespace ETicaret.Application.Features.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommandRequest : IRequest<UpdateCategoryCommandResponse>, IInvalidateCache
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Slug { get; set; }
        public string Description { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public Guid? ParentCategoryId { get; set; }
        public string InvalidateCacheKeyPrefix => CacheKeys.AllCategories.Key;
    }
}
