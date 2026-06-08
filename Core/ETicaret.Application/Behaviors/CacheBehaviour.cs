using ETicaret.Application.Abstractions.RedisCache;
using MediatR;

namespace ETicaret.Application.Behaviors
{
    public sealed class CacheBehaviour<TReq, TRes>(IRedisCacheService redisCacheService)
        : IPipelineBehavior<TReq, TRes>
        where TReq : IRequest<TRes>, ICacheable
        where TRes : class
    {
        public async Task<TRes> Handle(TReq request, RequestHandlerDelegate<TRes> next, CancellationToken cancellationToken)
        {
            if (request.IgnoreCacheRead is not true)
            {
                TRes? cachedValue = await redisCacheService.GetAsync<TRes>(request.CacheKey);
                if (cachedValue is not null)
                    return cachedValue;
            }
            var response = await next(cancellationToken);

            if (request.IgnoreCacheWrite is not true && response is not null)
            {
                await redisCacheService.SetAsync<TRes>(
                    request.CacheKey, response, DateTime.UtcNow.AddMinutes(request.ExpirationMinutes ?? 5));
            }
            return response ?? default!;
        }
    }
}
