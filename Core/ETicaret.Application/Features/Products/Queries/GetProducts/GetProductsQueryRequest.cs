using ETicaret.Application.Common.Abstractions.Caching;
using ETicaret.Application.Common.Constants;
using ETicaret.Application.Features.Products.DTOs;
using MediatR;

namespace ETicaret.Application.Features.Products.Queries.GetProducts
{
    public class GetProductsQueryRequest : BaseCacheableQuery<List<ProductListDto>>
    {
        protected override string CacheKeyPrefix => CacheKeys.AllProducts.Key;
        public override double? ExpirationMinutes => CacheKeys.AllProducts.Time;
    }
}
