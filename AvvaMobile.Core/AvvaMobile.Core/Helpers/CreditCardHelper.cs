using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AvvaMobile.Core.Helpers
{
    public static class CreditCardHelper
    {
        public static int GetCreditCardType(string cardNumber)
        {
            if (Regex.Match(cardNumber, @"^4[0-9]{12}(?:[0-9]{3})?$").Success)
            {
                return (int)CardTypesEnum.Visa;
            }

            if (Regex.Match(cardNumber, @"^(?:5[1-5][0-9]{2}|222[1-9]|22[3-9][0-9]|2[3-6][0-9]{2}|27[01][0-9]|2720)[0-9]{12}$").Success)
            {
                return (int)CardTypesEnum.MasterCard;
            }

            if (Regex.Match(cardNumber, @"^3[47][0-9]{13}$").Success)
            {
                return (int)CardTypesEnum.AmericanExpress;
            }

            if (Regex.Match(cardNumber, @"^6(?:011|5[0-9]{2})[0-9]{12}$").Success)
            {
                return (int)CardTypesEnum.Discover;
            }

            if (Regex.Match(cardNumber, @"^(?:2131|1800|35\d{3})\d{11}$").Success)
            {
                return (int)CardTypesEnum.JCB;
            }

            //if (Regex.Match(cardNumber, @"^(?:2131|1800|35\d{3})\d{11}$").Success)
            //{
            //    return (int)CardTypesEnum.Troy;
            //}

            return (int)CardTypesEnum.MasterCard;
        }
        public static string CryptedCard(this string cardNo) => $"**** **** **** {cardNo}";
    }
    public enum CardTypesEnum
    {
        MasterCard = 1,
        Visa = 2,
        AmericanExpress = 3,
        Discover = 4,
        JCB = 5,
        Troy = 6
    }
}
