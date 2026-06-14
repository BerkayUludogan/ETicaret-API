using ETicaret.Application.Common.Abstractions.Caching;
using ETicaret.Application.Common.Constants;
using ETicaret.Application.Features.Products.Commands.Common;
using MediatR;

namespace ETicaret.Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandRequest : ProductCommandBase, IRequest<UpdateProductCommandResponse>, IInvalidateCache
    {
        public Guid Id { get; set; }
        public string InvalidateCacheKeyPrefix => CacheKeys.AllProducts.Key;
    }
}
