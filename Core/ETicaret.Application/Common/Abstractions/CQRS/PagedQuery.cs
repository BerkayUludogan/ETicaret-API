namespace ETicaret.Application.Common.Abstractions.CQRS
{
    public class PagedQuery
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public string? Filter { get; set; }
        public string? SortOrder { get; set; }

        public PagedQuery(int pageNumber, int pageSize, string? filter, string? sortOrder)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            Filter = filter;
            SortOrder = sortOrder;
        }

        public PagedQuery()
            : this(1, 10, null, "desc")
        {
        }
    }
}
