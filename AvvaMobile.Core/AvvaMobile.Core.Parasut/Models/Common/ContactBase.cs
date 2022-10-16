namespace AvvaMobile.Core.Parasut.Models.Common
{
    public class ContactBase
    {
        /// <summary>
        /// Müşterinin Paraşüt'teki numarası. Yeni müşteri yaratırken null bırakılmalıdır.
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// Müşteri işlemlerinde bu alan olduğu gibi bırakılmalıdır.
        /// </summary>
        public string type { get; set; } = "contacts";
    }
}