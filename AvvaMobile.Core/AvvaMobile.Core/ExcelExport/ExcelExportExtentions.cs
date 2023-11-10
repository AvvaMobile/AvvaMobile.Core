using ArrayToExcel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvvaMobile.Core.ExcelExport;
public static class ExcelExportExtentions
{
    public static byte[] ExportToExcel<T>(this IEnumerable<T> items)
    {
        return items.ToExcel(schema =>
        {
            schema.ColumnName(columnInfo =>
            {
                foreach (var attribute in columnInfo.Member.GetCustomAttributesData())
                {
                    if (attribute.AttributeType.Name.Equals(nameof(ExcelColumnNameAttribute)))
                    {
                        return (string)attribute.ConstructorArguments[0].Value;
                    }
                }

                return columnInfo.Name;
            });
        });
    }
}
