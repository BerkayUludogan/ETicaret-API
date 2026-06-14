using ETicaret.Application.Common.Abstractions.Caching;
using ETicaret.Application.Common.Constants;
using MediatR;

namespace ETicaret.Application.Features.Products.Commands.DeleteProduct
{
    public class DeleteProductCommandRequest : IRequest<DeleteProductCommandResponse>, IInvalidateCache
    {
        public Guid Id { get; set; }

        public string InvalidateCacheKeyPrefix => CacheKeys.AllProducts.Key;
    }
}
