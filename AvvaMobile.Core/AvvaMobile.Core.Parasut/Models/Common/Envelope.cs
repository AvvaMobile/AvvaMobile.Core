namespace AvvaMobile.Core.Parasut.Models.Common
{
    /// <summary>
    /// Paraşüt API'nda tüm response'lar bir envelope içerisinde dönmektedir. Bu class ile dönen veri seti sarmalanarak alınır.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Envelope<T>
    {
        public T data { get; set; }
        public ResponseMeta meta { get; set; }
    }

    /// <summary>
    /// Envelope içerisinde bir liste dönüyor ise, sayfalamak gibi meta bilgilerini içerir.
    /// </summary>
    public class ResponseMeta
    {
        public int current_page { get; set; } = 1;
        public int total_pages { get; set; }
        public int total_count { get; set; }
        public int per_page { get; set; }
    }
}