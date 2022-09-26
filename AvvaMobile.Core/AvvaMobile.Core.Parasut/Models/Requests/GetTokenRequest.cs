namespace AvvaMobile.Core.Parasut.Models.Requests
{
    public class GetTokenRequest
    {
        public string grant_type { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string client_id { get; set; }
        public string client_secret { get; set; }
    }
}