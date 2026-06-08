using MediatR;

namespace ETicaret.Application.Features.Base
{
    public class BasePagedQuery<T>(int PageNumber, int PageSize, string? filter, string? sortOrder)
        : BasePagedQuery(PageNumber, PageSize, filter, sortOrder), IRequest<T> where T : class
    {
        public BasePagedQuery() : this(1, 10, filter: null, sortOrder: "desc") { }
    }
    public class BasePagedQuery(int pageNumber, int pageSize, string? filter, string? sortOrder)
    {
        public int? PageNumber { get; set; } = pageNumber;
        public int? PageSize { get; set; } = pageSize;
        public string? Filter { get; set; } = filter;
        public string? SortOrder { get; set; } = sortOrder;
    }
}
