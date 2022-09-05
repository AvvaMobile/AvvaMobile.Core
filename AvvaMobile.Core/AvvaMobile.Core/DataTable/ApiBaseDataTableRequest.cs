using Microsoft.AspNetCore.Http;

namespace AvvaMobile.Core.DataTable
{
    public class ApiBaseDataTableRequest
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string SortColumn { get; set; }
        public string SortColumnDir { get; set; }
        public List<SearchItem> SearchItems { get; set; }
        public int RecordsTotal { get; set; }
        public int Skip { get; set; }
        public int RecordsFiltered { get; set; }
        public int PageCount { get; set; }
    }
    public class SearchItem
    {
        public string SearchColumn { get; set; }
        public string SearchValue { get; set; }
    }
}
