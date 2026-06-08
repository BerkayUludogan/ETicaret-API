using ETicaret.Application.Abstractions.RedisCache;

namespace ETicaret.Application.Features.Base
{
    public class BaseCacheableQuery : BasePagedQuery,ICacheable
    {
        public virtual string CacheKey => $"{CacheKeyPrefix}_Page:{PageNumber}_Size:{PageSize}_Sort:{SortOrder}_Filter:{Filter}";
        protected virtual string CacheKeyPrefix { get; } = string.Empty;
        public virtual double? ExpirationMinutes { get; }
        public bool? IgnoreCacheRead { get; set; }
        public bool? IgnoreCacheWrite { get; set; }

        public BaseCacheableQuery(int pageNumber, int pageSize, string? filter, string? sortOrder)
           : base(pageNumber, pageSize, filter, sortOrder)
        {
        }
        public BaseCacheableQuery() : this(1, 10, null, "desc")
        {
        }
    }
}
