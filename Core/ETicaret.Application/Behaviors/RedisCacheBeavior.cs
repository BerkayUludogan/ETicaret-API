using ETicaret.Application.Abstractions.RedisCache;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ETicaret.Application.Behaviors
{
    public class RedisCacheBeavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        private readonly IRedisCacheService _redisCacheService;
        private readonly ILogger<RedisCacheBeavior<TRequest, TResponse>> _logger;

        public RedisCacheBeavior(ILogger<RedisCacheBeavior<TRequest, TResponse>> logger, IRedisCacheService redisCacheService)
        {
            _logger = logger;
            _redisCacheService = redisCacheService;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            try
            {
                if (request is ICacheableQuery query)
                {
                    var cacheKey = query.CacheKey;
                    var cacheTime = query.CacheTime;

                    var cachedData = await _redisCacheService.GetAsync<TResponse>(cacheKey);
                    if (cachedData is not null) return cachedData;

                    var response = await next();
                    if (response is not null)
                        await _redisCacheService.SetAsync(cacheKey, response, DateTime.UtcNow.AddMinutes(cacheTime));
                    return response;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return await next();
        }
    }
}
