using MediatR;

namespace ETicaret.Application.Features.Base
{
    public class CacheableQuery<T> : BaseCacheableQuery, IRequest<T>
    {

        protected CacheableQuery(int pageNumber, int pageSize, string? filter, string? sortOrder)
            : base(pageNumber, pageSize, filter, sortOrder) { }
        
        public CacheableQuery() : base()
        {
        }
    }
}
