using System;

namespace AvvaMobile.Core.Extensions
{
    public static class Decimal
    {
        /// <summary>
        /// Returns a string as Turkish Lira formatted.
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string ToTL(this decimal? val)
        {
            if (val == null)
            {
                return "0 TL.";
            }

            return val.Value.ToString("N2") + " TL.";
        }

        /// <summary>
        /// Returns a string as Turkish Lira formatted.
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string ToTL(this decimal val)
        {
            return val.ToString("N2") + " TL.";
        }

        /// <summary>
        /// Returns 0 if object is null, otherwise returns value of the object.
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static decimal DefaultIfEmpty(this decimal? val)
        {
            return val == null ? 0 : val.Value;
        }

        /// <summary>
        /// Formats the value as N0.
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string ToFormattedPrice(this decimal val)
        {
            return val.ToString("N0");
        }

        /// <summary>
        /// Formats the value as N0.
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string ToFormattedPrice(this decimal? val)
        {
            return val != null ? val.Value.ToString("N0") : "-";
        }

        /// <summary>
        /// Formats the value as N2.
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string ToFormattedDecimal(this decimal val)
        {
            return val.ToString("N2");
        }

        /// <summary>
        /// Formats the value as N2.
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string ToFormattedDecimal(this decimal? val)
        {
            if (!val.HasValue)
            {
                return string.Empty;
            }

            return val.Value.ToString("N2");
        }

        /// <summary>
        /// Formats the value as N2.
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string ToFormattedPercentage(this decimal? val)
        {
            if (!val.HasValue)
            {
                return string.Empty;
            }

            return ToFormattedPercentage(val.Value);
        }

        /// <summary>
        /// Formats the value as N2.
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string ToFormattedPercentage(this decimal val)
        {
            return $"% {(val * 100).ToString("N0")}";
        }

        /// <summary>
        /// Returns an hyphen as empty string indicator if object is null, otherwise returns value of the object.
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string ToEmptyStringIndicator(this decimal? val)
        {
            return val.HasValue ? val.Value.ToString() : "--";
        }
    }
}