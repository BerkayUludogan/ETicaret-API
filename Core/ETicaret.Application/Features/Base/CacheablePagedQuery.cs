using ETicaret.Models.Paging;
using MediatR;

namespace ETicaret.Application.Features.Base
{
    public class CacheablePagedQuery<T> : BaseCacheableQuery, IRequest<PagedResponse<T>>
    {
        protected CacheablePagedQuery(int pageNumber, int pageSize, string? filter, string? sortOrder)
              : base(pageNumber, pageSize, filter, sortOrder)
        {
        }
        public CacheablePagedQuery() : base()
        {
        }
    }
}
