namespace ETicaret.Models.Paging
{
    public class PagedResponse<T>(IList<T> pagedData,Page pageInfo)
    {
        public PagedResponse() : this([],new Page())
        {
            
        }
        public IList<T> PagedData { get; set; } = pagedData;
        public Page PageInfo { get; set; } = pageInfo;
    }
}
