using ETicaret.Models.Paging;
using MediatR;

namespace ETicaret.Application.Common.Abstractions.Caching
{
    public class CacheablePagedQuery<T> : CacheableQuery, IRequest<PagedResponse<T>>
    {
        public CacheablePagedQuery(int pageNumber, int pageSize, string? filter, string? sortOrder)
            : base(pageNumber, pageSize, filter, sortOrder)
        {
        }

        public CacheablePagedQuery()
            : base()
        {
        }
    }
}
