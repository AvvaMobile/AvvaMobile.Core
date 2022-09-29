namespace AvvaMobile.Core.Parasut.Models.Common
{
    public class Customer
    {
        /// <summary>
        /// Müşterinin Paraşüt'teki numarası. Yeni müşteri yaratırken null bırakılmalıdır.
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// Müşteri işlemlerinde bu alan olduğu gibi bırakılmalıdır.
        /// </summary>
        public string type { get; set; } = "contacts";

        /// <summary>
        /// Müşterinin tüm detay bilgileri bu alanın içerisinde listelenir.
        /// </summary>
        public CustomerAttributes attributes { get; set; } = new CustomerAttributes();
    }
}