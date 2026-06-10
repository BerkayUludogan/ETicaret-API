using ETicaret.Application.Common.Abstractions.Caching;
using MediatR;

namespace ETicaret.Application.Common.Behaviors
{
    public sealed class CacheInvalidationBehaviour<TReq, TRes>(
        ICacheService cacheService)
        : IPipelineBehavior<TReq, TRes>
        where TReq : IRequest<TRes>, IInvalidateCache
    {
        public async Task<TRes> Handle(
            TReq request,
            RequestHandlerDelegate<TRes> next,
            CancellationToken cancellationToken)
        {
            var response = await next();

            if (!string.IsNullOrWhiteSpace(request.InvalidateCacheKeyPrefix))
            {
                await cacheService.RemoveByPrefixAsync(
                    request.InvalidateCacheKeyPrefix);
            }

            return response;
        }
    }
}