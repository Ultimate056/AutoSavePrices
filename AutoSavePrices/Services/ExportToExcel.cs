using AutoSavePrices.Configurations;
using AutoSavePrices.Models;
using DevExpress.Export.Xl;
using DevExpress.XtraReports.ReportGeneration;
using DevExpress.XtraReports.UI;
using System;
using System.Data;
using System.IO;

namespace AutoSavePrices
{
    public class ExportToExcel
    {
        private readonly DataTable dataTable;

        public ExportToExcel(DataTable dt)
        {
            dataTable = dt;
        }

        public bool StartExport(RoutePrice path_price)
        {
            try
            {
                if (dataTable.Rows.Count < 1)
                {
                    if (Directory.Exists(path_price.PathDirectory))
                    {
                        Directory.Delete(path_price.PathDirectory);
                    }
                    throw new Exception("Нет прайсов для отправки");
                }

                // Create an exporter instance.
                IXlExporter exporter = XlExport.CreateExporter(XlDocumentFormat.Xlsx);

                var path = path_price.FullPath;

                // Проверка есть ли файл с таким названием

                using (FileStream stream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite))
                {

                    // Create a new document and begin to write it to the specified stream.
                    using (IXlDocument document = exporter.CreateDocument(stream))
                    {
                        // Add a new worksheet to the document.
                        using (IXlSheet sheet = document.CreateSheet())
                        {
                            // Specify the worksheet name.
                            sheet.Name = "Прайс-лист";

                            // Create the first column and set its width.
                            using (IXlColumn column = sheet.CreateColumn())
                            {
                                column.WidthInPixels = 100;
                            }

                            // Create the second column and set its width.
                            using (IXlColumn column = sheet.CreateColumn())
                            {
                                column.WidthInPixels = 400;
                            }

                            // Create the third column and set the specific number format for its cells.
                            using (IXlColumn column = sheet.CreateColumn())
                            {
                                column.WidthInPixels = 100;
                                column.Formatting = new XlCellFormatting();
                                column.Formatting.NumberFormat = @"_([$$-409]* #,##0.00_);_([$$-409]* \(#,##0.00\);_([$$-409]* ""-""??_);_(@_)";
                            }

                            // Specify cell font attributes.
                            XlCellFormatting cellFormatting = new XlCellFormatting();
                            cellFormatting.Font = new XlFont();
                            cellFormatting.Font.Name = "Arial";
                            cellFormatting.Font.Size = 10;
                            cellFormatting.Font.SchemeStyle = XlFontSchemeStyles.None;

                            // Specify formatting settings for the header row.
                            XlCellFormatting headerRowFormatting = new XlCellFormatting();
                            headerRowFormatting.CopyFrom(cellFormatting);
                            headerRowFormatting.Font.Bold = true;
                            headerRowFormatting.Font.Color = XlColor.DefaultForeground; //XlColor.FromTheme(XlThemeColor.Light1, 0.0);
                            headerRowFormatting.Fill = XlFill.SolidFill(XlColor.FromTheme(XlThemeColor.Accent4, 0.0));

                            // Create the header row.
                            using (IXlRow row = sheet.CreateRow())
                            {
                                XlCellFormatting cellHeaderFormatting = new XlCellFormatting();
                                cellFormatting.Font = new XlFont();
                                cellFormatting.Font.Name = "Arial";
                                cellFormatting.Font.Size = 10;
                                cellFormatting.Font.SchemeStyle = XlFontSchemeStyles.None;

                                row.HeightInPixels = 40;
                                row.ApplyFormatting(cellHeaderFormatting);
                                row.Formatting.Alignment = new XlCellAlignment();
                                row.Formatting.Alignment.VerticalAlignment = XlVerticalAlignment.Center;
                                row.Formatting.Alignment.HorizontalAlignment = XlHorizontalAlignment.Center;


                                foreach(var nameColumn in ConfExp.NameCols)
                                {
                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        cell.Value = nameColumn;
                                        cell.ApplyFormatting(headerRowFormatting);
                                    }
                                }
                            }

                            foreach (DataRow dtrow in dataTable.Rows)
                            {
                                using (IXlRow row = sheet.CreateRow())
                                {
                                    foreach(var valueColumn in ConfExp.NameColsDataTable)
                                    {
                                        using (IXlCell cell = row.CreateCell())
                                        {
                                            cell.Value = dtrow[valueColumn].ToString();
                                            cell.ApplyFormatting(cellFormatting);
                                        }
                                    }
                                }
                            }

                            // Enable AutoFilter for the created cell range.
                            sheet.AutoFilterRange = sheet.DataRange;
                        }
                    }
                }
                GC.Collect();
                return true;
            }
            catch (Exception ex)
            {
                UniLogger.WriteLog($"Экспорт DataTable в xlsx. Клиент {path_price.IdClient}", 1, ex.Message);
                return false;
            }

        }
    } 
}







// Specify formatting settings for the total row.
//XlCellFormatting totalRowFormatting = new XlCellFormatting();
//totalRowFormatting.CopyFrom(cellFormatting);
//totalRowFormatting.Font.Bold = true;
//totalRowFormatting.Fill = XlFill.SolidFill(XlColor.FromTheme(XlThemeColor.Accent5, 0.6));

// Create the total row.
//using (IXlRow row = sheet.CreateRow())
//{
//    using (IXlCell cell = row.CreateCell())
//    {
// cell.ApplyFormatting(totalRowFormatting);
//    }
//    using (IXlCell cell = row.CreateCell())
//    {
//        cell.Value = "Total amount";
// cell.ApplyFormatting(totalRowFormatting);
// cell.ApplyFormatting(XlCellAlignment.FromHV(XlHorizontalAlignment.Right, XlVerticalAlignment.Bottom));
//    }
//    using (IXlCell cell = row.CreateCell())
//    {
//        // Add values in the cell range C2 through C9 using the SUBTOTAL function.
// cell.SetFormula(XlFunc.Subtotal(XlCellRange.FromLTRB(2, 1, 2, 8), XlSummary.Sum, true));
// cell.ApplyFormatting(totalRowFormatting);
//    }
//}