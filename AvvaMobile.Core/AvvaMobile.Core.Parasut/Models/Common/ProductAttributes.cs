namespace AvvaMobile.Core.Parasut.Models.Common
{
    public class ProductAttributes
    {
        /// <summary>
        /// Ürün kodu.
        /// </summary>
        public string code { get; set; }

        /// <summary>
        /// Ürünün adı.
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// KDV oranı.
        /// </summary>
        public decimal vat_rate { get; set; }

        /// <summary>
        /// Birim.
        /// </summary>
        public string unit { get; set; }

        /// <summary>
        /// Ürün arşivlenme durumu.
        /// </summary>
        public bool archived { get; set; } = false;

        /// <summary>
        /// Vergiler hariç satış fiyatı.
        /// </summary>
        public decimal list_price { get; set; }

        /// <summary>
        /// Vergiler hariç alış fiyatı.
        /// </summary>
        public decimal buying_price { get; set; }

        /// <summary>
        /// Ürün barkod numarası.
        /// </summary>
        public string barcode { get; set; }

        /// <summary>
        /// Stok takibi yapılsın mı?
        /// </summary>
        public bool inventory_tracking { get; set; } = false;
    }
}