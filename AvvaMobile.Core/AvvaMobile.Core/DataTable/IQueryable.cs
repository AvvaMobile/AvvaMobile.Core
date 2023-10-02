using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;

namespace AvvaMobile.Core.DataTable
{
    public static class IQueryable
    {
        public static IQueryable<TSource> SortSkipTake<TSource>(this IQueryable<TSource> source, BaseDataTableRequest tableRequest)
        {
            if (!(string.IsNullOrEmpty(tableRequest.SortColumn) && string.IsNullOrEmpty(tableRequest.SortColumnDirection)))
            {
                source = source
                    .OrderBy(tableRequest.SortColumn + " " + tableRequest.SortColumnDirection);
            }

            source = source.Skip(tableRequest.Skip).Take(tableRequest.PageSize);

            return source;
        }
    }
}