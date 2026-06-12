using ETicaret.Application.Common.Abstractions.Caching;
using ETicaret.Application.Common.Constants;
using MediatR;

namespace ETicaret.Application.Features.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandRequest : IRequest<CreateCategoryCommandResponse>, IInvalidateCache
    {
        public required string Name { get; set; }
        public required string Slug { get; set; }
        public string? Description { get; set; }
        public Guid? ParentCategoryId { get; set; }
        public string InvalidateCacheKeyPrefix => CacheKeys.AllCategories.Key;
    }
}
