using ETicaret.Application.Common.Abstractions.Caching;
using Microsoft.Extensions.Caching.Memory;

namespace ETicaret.Infrastructure.Cache.Memory
{
    public class MemoryCacheService(IMemoryCache cache) : ICacheService
    {
        public Task<T?> GetAsync<T>(
            string key,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return Task.FromResult(cache.Get<T>(key));
        }

        public Task SetAsync<T>(
          string key,
     T value,
     TimeSpan expiration,
     CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var options = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(expiration);

            cache.Set(key, value, options);

            return Task.CompletedTask;
        }

        public Task RemoveAsync(
            string key,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            cache.Remove(key);

            return Task.CompletedTask;
        }

        public Task RemoveByPrefixAsync(
            string prefix,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            throw new NotSupportedException(
                "MemoryCache prefix bazlı silme işlemini desteklemez.");
        }
    }
}