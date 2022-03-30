using Microsoft.AspNetCore.Mvc.Rendering;

namespace AvvaMobile.Core.Extensions
{
    public static class SelectListItemExtension
    {
        public static List<SelectListItem> AddALLOption(this List<SelectListItem> list)
        {
            list.Insert(0, new SelectListItem { Text = "-- Tümü --", Value = "-1" });
            return list;
        }

        public static List<SelectListItem> AddSELECTOption(this List<SelectListItem> list)
        {
            list.Insert(0, new SelectListItem { Text = "-- Seçiniz --", Value = string.Empty });
            return list;
        }
    }
}