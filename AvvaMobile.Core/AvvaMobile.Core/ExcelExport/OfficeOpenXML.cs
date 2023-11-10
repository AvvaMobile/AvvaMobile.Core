using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AvvaMobile.Core.ExcelExport;
public sealed class OfficeOpenXML
{
    private static Lazy<OfficeOpenXML> _instance = new Lazy<OfficeOpenXML>(() => new OfficeOpenXML());

    private OfficeOpenXML()
    {

    }
    public static OfficeOpenXML GetInstance()
    {
        return _instance.Value;
    }

    public MemoryStream GetExcelStream(DataSet ds, bool firstRowAsHeader = false)
    {
        if (ds == null || ds.Tables.Count == 0)
        {
            return null;
        }

        MemoryStream stream = new MemoryStream();
        using (var excel = SpreadsheetDocument.Create(stream, SpreadsheetDocumentType.Workbook))
        {
            //create doc and workbook
            WorkbookPart workbookPart = excel.AddWorkbookPart();
            Workbook workbook = new Workbook();
            Sheets sheets = new Sheets();
            //loop all tables in the dataset
            for (int iTable = 0; iTable < ds.Tables.Count; iTable++)
            {
                var table = ds.Tables[iTable];
                //create sheet part
                WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                Worksheet worksheet = new Worksheet();
                SheetData data = new SheetData();
                List<Row> allRows = new List<Row>();

                //setting header of the sheet
                Row headerRow = new Row() { RowIndex = 1 };
                for (int iColumn = 0; iColumn < table.Columns.Count; iColumn++)
                {
                    var col = table.Columns[iColumn];
                    //if first row of table is not the header then set columns of table as header of sheet
                    if (!firstRowAsHeader)
                    {
                        headerRow.Append(new Cell
                        {
                            DataType = CellValues.String,
                            CellValue = new CellValue(col.ColumnName)
                        });
                    }
                    else
                    {
                        headerRow.Append(new Cell
                        {
                            DataType = CellValues.String,
                            CellValue = new CellValue(Convert.ToString(table.Rows[0][col]))
                        });
                    }
                }
                allRows.Add(headerRow);

                //setting other data rows
                if (table.Rows != null && table.Rows.Count != 0)
                {
                    for (int iRow = firstRowAsHeader ? 1 : 0; iRow < table.Rows.Count; iRow++)
                    {
                        var row = table.Rows[iRow];
                        Row valueRow = new Row { RowIndex = (uint)(iRow + (firstRowAsHeader ? 1 : 2)) };

                        for (int iColumn = 0; iColumn < table.Columns.Count; iColumn++)
                        {
                            var col = table.Columns[iColumn];
                            valueRow.Append(new Cell
                            {
                                DataType = Format(col.DataType),
                                CellValue = new CellValue(Convert.ToString(row[col]))
                            });
                        }
                        allRows.Add(valueRow);
                    }
                }

                //add rows to the data
                data.Append(allRows);
                worksheet.Append(data);
                worksheetPart.Worksheet = worksheet;
                worksheetPart.Worksheet.Save();

                //add worksheet to main sheets
                sheets.Append(new Sheet
                {
                    Name = string.IsNullOrWhiteSpace(table.TableName) ? "Sheet" + (iTable + 1) : table.TableName,
                    Id = workbookPart.GetIdOfPart(worksheetPart),
                    SheetId = (uint)iTable + 1
                });
            }//single table processing ends here

            //add created sheets to workbook
            workbook.Append(sheets);
            excel.WorkbookPart.Workbook = workbook;
            excel.WorkbookPart.Workbook.Save();


            excel.Dispose();
        }
        stream.Seek(0, SeekOrigin.Begin);
        stream.Capacity = (int)stream.Length;
        return stream;


    }
    public MemoryStream GetExcelStream(System.Data.DataTable dt, bool firstRowAsHeader = false)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add(dt);
        return GetExcelStream(ds, firstRowAsHeader);
    }



    #region Excel Helpers

    CellValues Format(Type t)
    {

        switch (t.ToString())
        {

            case "System.String":
                return CellValues.String;
            case "System.DateTime":
                return CellValues.Date;
            case "System.Boolean":
                return CellValues.Boolean;
            case "System.Int16":
                return CellValues.Number;
            case "System.Int32":
                return CellValues.Number;
            case "System.Int64":
                return CellValues.Number;
            case "System.UInt16":
                return CellValues.Number;
            case "System.UInt32":
                return CellValues.Number;
            case "System.UInt64":
                return CellValues.Number;
            case "System.Decimal":
                return CellValues.Number;
            case "System.Double":
                return CellValues.Number;
            case "System.Single":
                return CellValues.Number;
            default:
                return CellValues.String;
        }
    }
    #endregion
}