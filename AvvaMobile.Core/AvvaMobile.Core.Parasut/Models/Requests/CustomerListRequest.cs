namespace AvvaMobile.Core.Parasut.Models.Requests
{
    public class CustomerListRequest
    {
        public string name { get; set; }
        public string email { get; set; }
        public string tax_office { get; set; }
        public string tax_number { get; set; }
        public int page_number { get; set; } = 1;
    }
}