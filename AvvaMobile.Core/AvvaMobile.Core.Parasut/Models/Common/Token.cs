namespace AvvaMobile.Core.Parasut.Models.Common
{
    /// <summary>
    /// OAuth ile giriş yapıldıktan sonra tüm güvenlik bilgilerini tutar.
    /// </summary>
    public class Token
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
        public string refresh_token { get; set; }
        public string scope { get; set; }
        public int created_at { get; set; }
    }
}