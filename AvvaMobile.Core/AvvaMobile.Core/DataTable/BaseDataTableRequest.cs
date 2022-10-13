using Microsoft.AspNetCore.Http;

namespace AvvaMobile.Core.DataTable
{
    public class BaseDataTableRequest
    {
        public BaseDataTableRequest(HttpRequest request)
        {
            Draw = request.Form["draw"].FirstOrDefault();
            Start = request.Form["start"].FirstOrDefault();
            Length = request.Form["length"].FirstOrDefault() ?? "20";
            SortColumn = request.Form["columns[" + request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            SortColumnDirection = request.Form["order[0][dir]"].FirstOrDefault();
            SearchValue = request.Form["search[value]"].FirstOrDefault();
            PageSize = Length != null ? Convert.ToInt32(Length) : 0;
            Skip = Start != null ? Convert.ToInt32(Start) : 0;
        }

        public int? Id { get; set; }
        public string Draw { get; set; }
        public string Start { get; set; }
        public string Length { get; set; }
        public string SortColumn { get; set; }
        public string SortColumnDirection { get; set; }
        public string SearchValue { get; set; }
        public DateTime? FilterStartDate { get; set; }
        public DateTime? FilterEndDate { get; set; }
        public int? PageSize { get; set; }
        public int Skip { get; set; }
        public int? RecordsTotal { get; set; }
        public string selectedDate { get; set; }
        public int? selectedStatus { get; set; }
        public bool SearchAllColumns { get; set; }
        public List<SearchItem> SearchItems { get; set; }
    }
}
