using ETicaret.Application.Common.Abstractions.Caching;
using ETicaret.Application.Common.Constants;
using ETicaret.Application.Features.Products.Commands.Common;
using MediatR;

namespace ETicaret.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommandRequest : ProductCommandBase, IRequest<CreateProductCommandResponse>, IInvalidateCache
    {
        public string InvalidateCacheKeyPrefix => CacheKeys.AllProducts.Key;
    }
}
