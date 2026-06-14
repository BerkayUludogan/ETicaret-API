using ETicaret.Application.Common.Abstractions.CQRS;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Serialization;

namespace ETicaret.Application.Common.Abstractions.Caching
{
    public class CacheableQuery : PagedQuery, ICacheable
    {
        protected virtual string CacheKeyPrefix { get; } = string.Empty;

        public virtual string CacheKey =>
            $"{CacheKeyPrefix}_Page:{PageNumber}_Size:{PageSize}_Sort:{SortOrder}_Filter:{Filter}";

        public virtual double? ExpirationMinutes { get; }

        [JsonIgnore]
        [BindNever]
        public bool? IgnoreCacheRead { get; set; }
        [JsonIgnore]
        [BindNever]
        public bool? IgnoreCacheWrite { get; set; }

        public CacheableQuery(int pageNumber, int pageSize, string? filter, string? sortOrder)
            : base(pageNumber, pageSize, filter, sortOrder)
        {
        }

        public CacheableQuery()
            : this(1, 10, null, "desc")
        {
        }
    }
}
