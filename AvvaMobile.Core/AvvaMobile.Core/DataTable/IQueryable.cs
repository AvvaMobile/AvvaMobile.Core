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

        public static IQueryable<TSource> Search<TSource>(this IQueryable<TSource> query, string keyword)
        {
            var searchableProperties = query.ElementType.GetProperties().Where(x => x.PropertyType.Name.Equals(nameof(String)));

            var predicate = string.Join(" || ", searchableProperties.Select(x => $"{x.Name}.Contains(@0)"));

            query = query.Where(predicate, keyword);

            return query;
        }
    }
}