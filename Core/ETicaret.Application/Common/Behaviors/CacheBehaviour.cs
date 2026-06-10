using ETicaret.Application.Common.Abstractions.Caching;
using MediatR;

namespace ETicaret.Application.Common.Behaviors
{
    public sealed class CacheBehaviour<TReq, TRes>(ICacheService cacheService)
        : IPipelineBehavior<TReq, TRes>
        where TReq : IRequest<TRes>, ICacheable
        where TRes : class
    {
        public async Task<TRes> Handle(
            TReq request,
            RequestHandlerDelegate<TRes> next,
            CancellationToken cancellationToken)
        {
            if (request.IgnoreCacheRead is not true)
            {
                var cachedValue = await cacheService.GetAsync<TRes>(
                    request.CacheKey,
                    cancellationToken);

                if (cachedValue is not null)
                    return cachedValue;
            }

            var response = await next(cancellationToken);

            if (request.IgnoreCacheWrite is not true && response is not null)
            {
                await cacheService.SetAsync(
                    request.CacheKey,
                    response,
                    TimeSpan.FromMinutes(request.ExpirationMinutes ?? 5),
                    cancellationToken);
            }

            return response ?? default!;
        }
    }
}