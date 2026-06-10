using MediatR; 

namespace ETicaret.Application.Common.Abstractions.CQRS
{
    public class BasePagedQuery<T> : PagedQuery, IRequest<T>
      where T : class
    {
        public BasePagedQuery(int pageNumber, int pageSize, string? filter, string? sortOrder)
            : base(pageNumber, pageSize, filter, sortOrder)
        {
        }

        public BasePagedQuery()
            : base()
        {
        }
    }
}
