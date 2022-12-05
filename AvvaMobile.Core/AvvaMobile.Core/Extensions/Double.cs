using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvvaMobile.Core.Extensions
{
    public static class Double
    {
        /// <summary>
        /// Returns a string as Turkish Lira formatted.
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string ToTL(this double? val)
        {
            if (val == null)
            {
                return "0 TL.";
            }

            return val.Value.ToString("N2");
        }

        /// <summary>
        /// Returns 0 if object is null, otherwise returns value of the object.
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static double DefaultIfEmpty(this double? val)
        {
            return val == null ? 0 : val.Value;
        }

        /// <summary>
        /// Formats the value as N0.
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string ToFormattedPrice(this double val)
        {
            return val.ToString("N0");
        }

        /// <summary>
        /// Formats the value as N0.
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string ToFormattedPrice(this double? val)
        {
            return val != null ? val.Value.ToString("N0") : "-";
        }

        /// <summary>
        /// Formats the value as N2.
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string ToFormattedDouble(this double val)
        {
            return val.ToString("N2");
        }

        /// <summary>
        /// Formats the value as N2.
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string ToFormattedDouble(this double? val)
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
        public static string ToFormattedPercentage(this double? val)
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
        public static string ToFormattedPercentage(this double val)
        {
            return $"% {val.ToString("N2")}";
        }

        /// <summary>
        /// Returns an hyphen as empty string indicator if object is null, otherwise returns value of the object.
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string ToEmptyStringIndicator(this double? val)
        {
            return val.HasValue ? val.Value.ToString() : "--";
        }
    }
}