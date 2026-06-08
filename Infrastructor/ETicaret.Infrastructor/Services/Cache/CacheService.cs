using ETicaret.Application.Abstractions.RedisCache;
using Microsoft.Extensions.Caching.Memory; 

namespace ETicaret.Infrastructure.Services.Cache
{
    public class CacheService(IMemoryCache cache) : ICacheService
    {
        public void Set<T>(string key, T value, TimeSpan? absoluteExpireTime = null, TimeSpan? unusedExpireTime = null)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions();
            if (absoluteExpireTime.HasValue)
                cacheEntryOptions.SetAbsoluteExpiration(absoluteExpireTime.Value);
            if (unusedExpireTime.HasValue)
                cacheEntryOptions.SetSlidingExpiration(unusedExpireTime.Value);
            cache.Set(key, value, cacheEntryOptions);
        }
        public T? Get<T>(string key) => cache.Get<T>(key);

        public void Remove(string key)
        {
            cache.Remove(key);
        }
    }
}
