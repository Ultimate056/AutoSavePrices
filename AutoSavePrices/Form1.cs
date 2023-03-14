
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace AutoSavePrices
{
    public partial class Form1 : Form
    {
        QAllClients needClients;
        public Form1(QAllClients clients)
        {
            InitializeComponent();
            needClients = clients;
        }
        public Form1()
        {
            InitializeComponent();
        }


        public void Start()
        {
            // Первые клиенты
            Client[] topClients = needClients.GetClients();

            // Запускаем первые 4 потока на формирование
            MainExecutor ex = new MainExecutor();
            ex.Start(topClients);
            comboBox1.Items.AddRange(topClients.Select(x => x.idKontr.ToString()).ToArray());
            // В главном потоке отслеживаем состояния потоков
            while(true)
            {
                if(ex.PoolTasks.Count > 0)
                {
                    for(int i = 0; i < ex.PoolTasks.Count; i++)
                    {
                        if (ex.PoolTasks[i].IsCompleted)
                        {
                            ex.PoolTasks[i].Dispose();
                            ex.PoolTasks.RemoveAt(i);

                            if(needClients.countClients > 0)
                            {
                                var nextClient = needClients.Next();
                                ex.GenerateTask(nextClient);
                                i = 0;
                                comboBox1.Items.Add(nextClient.idKontr.ToString());
                            }

                        }
                    }
                }
                else
                {
                    if(needClients.countClients > 0)
                    {
                        for(int i = 0; i < needClients.countClients; i++)
                        {
                            ex.GenerateTask(needClients.Next());
                        }
                    }
                    else
                    {
                        //Task.WaitAll(ex.PoolTasks.ToArray());
                        MessageBox.Show("success");
                        break;
                    }
                }
            }

            GC.Collect();


        }






        ///// <summary>
        ///// Экспортирует datatable в xlsx
        ///// </summary>
        //private void ExportToExcel(DataTable dt)
        //{
        //    Random rand = new Random();
        //    int a = rand.Next();

        //    GridControl control = new GridControl();

        //    GridBand gridBand1;
        //    GridBand gridBand2;

        //    BandedGridView gv = new BandedGridView(control);

        //    CreateBands(out gridBand1, out gridBand2, ref gv);
        //    CreateColumns(gridBand1, gridBand2);

        //    gv.GridControl = control;
        //    control.MainView = gv;


        //    ReportGenerationOptions ropt = new ReportGenerationOptions();
        //    //ropt.AutoFitToPageWidth = DevExpress.Utils.DefaultBoolean.True;
        //    ropt.UsePrintAppearances = DevExpress.Utils.DefaultBoolean.True;

        //    XtraReport report = ReportGenerator.GenerateReport(gv, ropt);

        //    DevExpress.Export.ExportSettings.DefaultExportType = DevExpress.Export.ExportType.WYSIWYG;
        

        //    XlsxExportOptions opt = new XlsxExportOptions()
        //    {
        //        ExportMode = XlsxExportMode.SingleFile,
        //        ShowGridLines = true,
        //        FitToPrintedPageHeight = true,
        //        SheetName = "Прайс-лист ТЕСТ"
        //    };

        //    report.DataSource = dt;
        //    report.ExportToXlsx("E:/PricesAutoSave/" + a + ".xlsx", opt);

        //    dt.Dispose();
        //    control.Dispose();
        //    gv.Dispose();
        //    report.Dispose();
        //    GC.Collect();
            
        //}

        /// <summary>
        /// Отправка xlsx файла через почтовый сервер клиенту
        /// </summary>
        private void SendEmail()
        {

        }



        /// <summary>
        /// Сохранение макета gridView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveLayoutButton_Click(object sender, EventArgs e)
        {

        }




        /// <summary>
        /// Формирование прайсов и инициализация gridView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GenericPricesButton_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            Start();

            Cursor.Current = Cursors.Default;
        }
    }
}
