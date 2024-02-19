namespace CleanCode.Core.Models
{
    public class PaginationQuery
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public bool IncludeTotalCount { get; set; } = true;
        public string Sortby { get; set; }
        public string SortOrder { get; set; }
        public string Filterby { get; set; }
        public string FilterValue { get; set; }

        public PaginationQuery()
        {
            PageNumber = 1;
            PageSize = 50;
        }

        public PaginationQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber < 1 ? 1 : pageNumber;
            PageSize = pageSize > 50 ? 50 : pageSize;
        }

        public PaginationQuery(int pageNumber, int pageSize, string sortby, string sortOrder)
        {
            PageNumber = pageNumber < 1 ? 1 : pageNumber;
            PageSize = pageSize > 50 ? 50 : pageSize;
            Sortby = sortby;
            SortOrder = sortOrder == null ? "asc" : sortOrder;
        }

        public PaginationQuery(int pageNumber, int pageSize, string sortby, string sortOrder,string filterby, string filterValue)
        {
            PageNumber = pageNumber < 1 ? 1 : pageNumber;
            PageSize = pageSize > 50 ? 50 : pageSize;
            Sortby = sortby;
            SortOrder = sortOrder == null ? "asc" : sortOrder;
            Filterby = filterby;
            FilterValue = filterValue;
        }
    }
}
