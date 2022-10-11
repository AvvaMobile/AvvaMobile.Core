namespace AvvaMobile.Core.Parasut.Models.Common
{
    public class SalesInvoice
    {
        /// <summary>
        /// Faturanın Paraşüt'teki numarası. Yeni fatura yaratırken null bırakılmalıdır.
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// Fatura işlemlerinde bu alan olduğu gibi bırakılmalıdır.
        /// </summary>
        public string type { get; set; } = "sales_invoices";

        /// <summary>
        /// Faturanın tüm detay bilgileri bu alanın içerisinde listelenir.
        /// </summary>
        public SalesInvoiceAttributes attributes { get; set; } = new SalesInvoiceAttributes();
    }
}