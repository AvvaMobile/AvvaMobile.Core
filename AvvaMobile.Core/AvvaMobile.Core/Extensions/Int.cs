namespace AvvaMobile.Core.Extensions
{
    public static class Int
    {
        public static string ToTL(this int? val)
        {
            if (val == null)
            {
                return "0 TL.";
            }
            return val.Value.ToString("N0") + " TL.";
        }

        public static string ToTL(this int val)
        {
            return val.ToString("N0") + " TL.";
        }

        public static bool IsALLSelected(this int val)
        {
            return val.Equals(0);
        }

        /// <summary>
        /// Returns 0 if object is null, otherwise returns value of the object.
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static int DefaultIfEmpty(this int? val)
        {
            return val == null ? 0 : val.Value;
        }

        /// <summary>
        /// Formats the value as N0.
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string ToFormattedPrice(this int val)
        {
            return val.ToString("N0");
        }

        /// <summary>
        /// Formats the value as N0.
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string ToFormattedPrice(this int? val)
        {
            return val != null ? val.Value.ToString("N0") : "-";
        }

        /// <summary>
        /// Returns an hyphen as empty string indicator if object is null, otherwise returns value of the object.
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string ToEmptyStringIndicator(this int? val)
        {
            return val.HasValue ? val.Value.ToString() : "--";
        }
    }
}