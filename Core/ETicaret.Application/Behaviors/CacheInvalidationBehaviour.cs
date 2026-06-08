using ETicaret.Application.Abstractions.RedisCache;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETicaret.Application.Behaviors
{
    public class CacheInvalidationBehaviour<TReq, TRes>(IRedisCacheService redisCacheService)
        : IPipelineBehavior<TReq, TRes>
            where TReq : IRequest<TRes>, IInvalidateableCache
    {
        public async Task<TRes> Handle(TReq request, RequestHandlerDelegate<TRes> next, CancellationToken cancellationToken)
        {
            var response = await next();
            if (!string.IsNullOrWhiteSpace(request.InvalidateCacheKeyPrefix))
            {
                await redisCacheService.RemoveByPrefixAsync(request.InvalidateCacheKeyPrefix);
            }
            return response;
        }
    }
}
