using Amazon.Runtime.Internal;
using AvvaMobile.Core.DataTable;
using LinqKit;
using Microsoft.EntityFrameworkCore;
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

            var predicate = PredicateBuilder.New<T>(true);

            foreach (var item in tableRequest.SearchItems)
            {
                if (DateTime.TryParse(item.SearchValue.ToString(), out var dt))
                {
                    predicate = predicate.Or(x => EF.Property<DateTime>(x, item.SearchColumn).ToString().Contains(item.SearchValue.ToString()));
                }
                else if (bool.TryParse(item.SearchValue.ToString(), out var bl))
                {
                    predicate = predicate.Or(x => EF.Property<bool>(x, item.SearchColumn).ToString().Contains(item.SearchValue.ToString()));
                }
                else if (int.TryParse(item.SearchValue.ToString(), out var s))
                {
                    predicate = predicate.Or(x => EF.Property<int>(x, item.SearchColumn).ToString().Contains(item.SearchValue.ToString()));
                }
                else if (double.TryParse(item.SearchValue.ToString(), out var db))
                {
                    predicate = predicate.Or(x => EF.Property<double>(x, item.SearchColumn).ToString().Contains(item.SearchValue.ToString()));
                }
                else if (item.SearchValue.ToString() == "null")
                {
                    predicate = predicate.Or(x => EF.Property<object>(x, item.SearchColumn) == null);
                }
                else
                {
                    predicate = predicate.Or(x => EF.Property<string>(x, item.SearchColumn).Contains(item.SearchValue.ToString()));
                }
            }

            list = list.Where(predicate);

            tableRequest.RecordsTotal = list.Count();
            tableRequest.Skip = tableRequest.PageSize * (tableRequest.Page - 1);
            list = list.Skip(tableRequest.Skip).Take(tableRequest.PageSize);

            tableRequest.RecordsFiltered = list.Count();
            tableRequest.PageCount = (int)Math.Ceiling((decimal)tableRequest.RecordsTotal / (decimal)tableRequest.PageSize);
            return list;
        }
    }
}