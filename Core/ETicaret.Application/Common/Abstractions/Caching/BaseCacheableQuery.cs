using MediatR; 

namespace ETicaret.Application.Common.Abstractions.Caching
{
    public class BaseCacheableQuery<T> : CacheableQuery, IRequest<T>
    {
        public BaseCacheableQuery(int pageNumber, int pageSize, string? filter, string? sortOrder)
            : base(pageNumber, pageSize, filter, sortOrder)
        {
        }

        public BaseCacheableQuery()
            : base()
        {
        }
    }
}
