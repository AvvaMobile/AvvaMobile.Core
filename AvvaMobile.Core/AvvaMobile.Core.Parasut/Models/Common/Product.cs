namespace AvvaMobile.Core.Parasut.Models.Common
{
    public class Product
    {
        /// <summary>
        /// Ürünün Paraşüt'teki numarası. Yeni ürün yaratırken null bırakılmalıdır.
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// Ürün işlemlerinde bu alan olduğu gibi bırakılmalıdır.
        /// </summary>
        public string type { get; set; } = "products";

        /// <summary>
        /// Ürünün tüm detay bilgileri bu alanın içerisinde listelenir.
        /// </summary>
        public ProductAttributes attributes { get; set; } = new ProductAttributes();
    }
}