namespace ETicaret.Application.Abstractions.RedisCache
{
    public interface ICacheable
    {
        public string CacheKey { get; }
        public double? ExpirationMinutes { get; }
        public bool? IgnoreCacheRead { get; set; }
        public bool? IgnoreCacheWrite { get; set; }
    }
}
