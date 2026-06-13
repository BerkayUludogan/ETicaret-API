using ETicaret.Application.Common.Abstractions.Caching;
using ETicaret.Application.Common.Constants;
using MediatR;

namespace ETicaret.Application.Features.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryCommandRequest : IRequest<DeleteCategoryCommandResponse>, IInvalidateCache
    {
        public Guid Id { get; set; }
        public string InvalidateCacheKeyPrefix => CacheKeys.AllCategories.Key;
    }
}
