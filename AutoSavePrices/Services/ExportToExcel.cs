using DevExpress.XtraReports.ReportGeneration;
using DevExpress.XtraReports.UI;
using System;
using System.Data;

namespace AutoSavePrices
{
    public class ExportToExcel
    {
        private readonly DataTable dataTable;

        public ExportToExcel(DataTable dt)
        {
            dataTable = dt;
        }

        public bool StartExport(string NameFile)
        {
            try
            {
                var gv = ConfigurationExport.CommonGridView;
                var ropt = ConfigurationExport.CommonReportOptions;
                var xlsxopt = ConfigurationExport.XlsxOpt;
                var path = ConfigurationExport.XlsxExportPath;

                XtraReport report = ReportGenerator.GenerateReport(gv, ropt);
                report.DataSource = dataTable;
                report.ExportToXlsx(path + NameFile + ".xlsx", xlsxopt);

                report.Dispose();
                dataTable.Dispose();
                GC.Collect();

                return true;
            }
            catch(Exception ex)
            {
                UniLogger.WriteLog("Экспорт DataTable в xlsx", 1, ex.Message);
                return false;
            }

        }

    }

    
}
