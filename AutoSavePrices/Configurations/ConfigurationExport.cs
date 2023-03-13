using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.ReportGeneration;
using System;


namespace AutoSavePrices
{
    public static class ConfigurationExport
    {
        public static BandedGridView CommonGridView;

        public static ReportGenerationOptions CommonReportOptions;

        public static XlsxExportOptions XlsxOpt;

        public static readonly string XlsxExportPath = "E:/PricesAutoSave/";

        static ConfigurationExport()
        {
            try
            {
                GridControl control = new GridControl();

                GridBand gridBand1;
                GridBand gridBand2;

                CommonGridView = new BandedGridView(control);

                CreateBands(out gridBand1, out gridBand2, ref CommonGridView);
                CreateColumns(gridBand1, gridBand2);

                CommonGridView.GridControl = control;
                control.MainView = CommonGridView;

                CommonReportOptions = new ReportGenerationOptions();
                //ropt.AutoFitToPageWidth = DevExpress.Utils.DefaultBoolean.True;
                CommonReportOptions.UsePrintAppearances = DevExpress.Utils.DefaultBoolean.True;

                XlsxOpt = new XlsxExportOptions()
                {
                    ExportMode = XlsxExportMode.SingleFile,
                    ShowGridLines = true,
                    FitToPrintedPageHeight = true,
                    SheetName = "Прайс-лист ТЕСТ"
                };
            }
            catch (Exception ex)
            {
                UniLogger.WriteLog("Конфигурация экспорта", 1, ex.Message);
            }

        }

        private static void CreateBands(out GridBand gridBand1, out GridBand gridBand2, ref BandedGridView bgv)
        {
            gridBand1 = new GridBand();
            gridBand2 = new GridBand();
            gridBand1.Name = "BAND1";
            gridBand1.Caption = "Бренд";
            gridBand1.AppearanceHeader.BackColor2 = System.Drawing.Color.Yellow;
            gridBand1.AppearanceHeader.Font = new System.Drawing.Font("Arial", 20);
            gridBand2.Name = "BAND2";
            gridBand2.Caption = "Товар";


            bgv.Bands.Add(gridBand1);
            bgv.Bands.Add(gridBand2);
        }
        private static void CreateColumns(GridBand gridBand1, GridBand gridBand2)
        {
            BandedGridColumn bandedGridColumn1 = new BandedGridColumn();
            BandedGridColumn bandedGridColumn2 = new BandedGridColumn();
            BandedGridColumn bandedGridColumn3 = new BandedGridColumn();
            BandedGridColumn bandedGridColumn4 = new BandedGridColumn();
            BandedGridColumn bandedGridColumn5 = new BandedGridColumn();
            BandedGridColumn bandedGridColumn6 = new BandedGridColumn();
            BandedGridColumn bandedGridColumn7 = new BandedGridColumn();
            BandedGridColumn bandedGridColumn8 = new BandedGridColumn();
            BandedGridColumn bandedGridColumn9 = new BandedGridColumn();

            bandedGridColumn1.Caption = "ID товара";
            bandedGridColumn1.FieldName = "idTov";
            bandedGridColumn2.Caption = "Наименование";
            bandedGridColumn2.FieldName = "n_tov";
            bandedGridColumn3.Caption = "Артикул";
            bandedGridColumn3.FieldName = "id_tov_oem_short";
            bandedGridColumn4.Caption = "Цена Р1";
            bandedGridColumn4.FieldName = "price";
            bandedGridColumn5.Caption = "Цена min";
            bandedGridColumn5.FieldName = "minprice";
            bandedGridColumn6.Caption = "Бренд";
            bandedGridColumn6.FieldName = "tm_name";
            bandedGridColumn7.Caption = "ID бренда";
            bandedGridColumn7.FieldName = "id_tm";
            bandedGridColumn8.Caption = "pricerrc";
            bandedGridColumn8.FieldName = "pricerrc";
            bandedGridColumn9.Caption = "min price бренда";
            bandedGridColumn9.FieldName = "minpricebrand";

            bandedGridColumn1.Visible = true;
            bandedGridColumn2.Visible = true;
            bandedGridColumn3.Visible = true;
            bandedGridColumn4.Visible = true;
            bandedGridColumn5.Visible = true;
            bandedGridColumn6.Visible = true;
            bandedGridColumn7.Visible = true;
            bandedGridColumn8.Visible = true;
            bandedGridColumn9.Visible = true;


            gridBand1.Columns.Add(bandedGridColumn7);
            gridBand1.Columns.Add(bandedGridColumn6);
            gridBand1.Columns.Add(bandedGridColumn9);

            gridBand2.Columns.Add(bandedGridColumn1);
            gridBand2.Columns.Add(bandedGridColumn2);
            gridBand2.Columns.Add(bandedGridColumn3);
            gridBand2.Columns.Add(bandedGridColumn4);
            gridBand2.Columns.Add(bandedGridColumn5);
            gridBand2.Columns.Add(bandedGridColumn8);

        }


    }
}
