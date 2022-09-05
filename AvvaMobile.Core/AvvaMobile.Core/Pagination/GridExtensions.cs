using AvvaMobile.Core.DataTable;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace AvvaMobile.Core.Pagination
{
    public static class GridExtensions
    {
        public static IQueryable<T> GridFilter<T>(this IQueryable<T> list, BaseDataTableRequest tableRequest)
        {
            if (!(string.IsNullOrEmpty(tableRequest.SortColumn) && string.IsNullOrEmpty(tableRequest.SortColumnDirection)))
            {
                list = list.OrderBy(tableRequest.SortColumn + " " + tableRequest.SortColumnDirection);
            }

            if (tableRequest.SearchItems != null)
            {
                foreach (var item in tableRequest.SearchItems)
                {
                    if (DateTime.TryParse(item.SearchValue, out var datetimeValue))
                    {
                        list = list.Where($"{item.SearchColumn} = @0", datetimeValue);
                        continue;
                    }
                    else if (bool.TryParse(item.SearchValue, out var boolValue))
                    {
                        list = list.Where($"{item.SearchColumn} = @0", boolValue);
                        continue;
                    }
                    else if (int.TryParse(item.SearchValue, out var intValue))
                    {
                        list = list.Where($"{item.SearchColumn} = @0", intValue);
                        continue;
                    }
                    else if (double.TryParse(item.SearchValue, out var doubleValue))
                    {
                        list = list.Where($"{item.SearchColumn} = @0", doubleValue);
                        continue;
                    }

                    var where = $"{item.SearchColumn}.Contains(@0)";
                    list = list.Where(where, item.SearchValue);
                }
            }

            list = list.Skip(tableRequest.Skip).Take(tableRequest.PageSize ?? 20);

            return list;
        }
        public static IQueryable<T> ApiGridFilter<T>(this IQueryable<T> list, ApiBaseDataTableRequest tableRequest)
        {
            if (!(string.IsNullOrEmpty(tableRequest.SortColumn) && string.IsNullOrEmpty(tableRequest.SortColumnDir)))
            {
                list = list.OrderBy(tableRequest.SortColumn + " " + tableRequest.SortColumnDir);
            }
            if (!tableRequest.SearchItems.Contains(null))
            {
                foreach (var item in tableRequest.SearchItems)
                {
                    if (DateTime.TryParse(item.SearchValue.ToString(), out var dt))
                    {
                        list = list.Where($"{item.SearchColumn} = @0", dt);
                        continue;
                    }
                    if (bool.TryParse(item.SearchValue.ToString(), out var bl))
                    {
                        list = list.Where($"{item.SearchColumn} = @0", bl);
                        continue;
                    }
                    if (int.TryParse(item.SearchValue.ToString(), out var s))
                    {
                        list = list.Where($"{item.SearchColumn} = @0", s);
                        continue;
                    }
                    if (double.TryParse(item.SearchValue.ToString(), out var db))
                    {
                        list = list.Where($"{item.SearchColumn} = @0", db);
                        continue;
                    }
                    if (item.SearchValue.ToString() == "null")
                    {
                        list = list.Where($"{item.SearchColumn} = null");
                        continue;
                    }

                    var where = $"{item.SearchColumn}.Contains(@0)";
                    list = list.Where(where, item.SearchValue);
                }
            }


            tableRequest.RecordsTotal = list.Count();

            list = list.Skip(tableRequest.Skip).Take(tableRequest.PageSize);

            tableRequest.RecordsFiltered = list.Count();
            tableRequest.PageCount = (int)Math.Ceiling((decimal)tableRequest.RecordsTotal / (decimal)tableRequest.PageSize);
            return list;
        }
    }
}