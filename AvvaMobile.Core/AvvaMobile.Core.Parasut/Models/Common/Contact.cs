namespace AvvaMobile.Core.Parasut.Models.Common
{
    public class Contact : ContactBase
    {
        /// <summary>
        /// Müşterinin tüm detay bilgileri bu alanın içerisinde listelenir.
        /// </summary>
        public ContactAttributes attributes { get; set; } = new ContactAttributes();
    }
}