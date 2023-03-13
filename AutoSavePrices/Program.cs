using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoSavePrices
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            QAllClients allClients = new QAllClients(4);


            allClients.Add(new Client
            {
                idKontr = 554890,
                idKategory = 21,
                idDirect = 60,
                idCur = 0,
                inPrice = 1,
                idTerritory = 1,
                fnotWithoutGTD = 0
            });
            allClients.Add(new Client
            {
                idKontr = 549460,
                idKategory = 31,
                idDirect = 60,
                idCur = 0,
                inPrice = 1,
                idTerritory = 1,
                fnotWithoutGTD = 0
            });
            allClients.Add(new Client
            {
                idKontr = 553592,
                idKategory = 4,
                idDirect = 60,
                idCur = 0,
                inPrice = 1,
                idTerritory = 1,
                fnotWithoutGTD = 0
            });
            allClients.Add(new Client
            {
                idKontr = 548913,
                idKategory = 20,
                idDirect = 60,
                idCur = 0,
                inPrice = 1,
                idTerritory = 1,
                fnotWithoutGTD = 0
            });
            allClients.Add(new Client
            {
                idKontr = 548966,
                idKategory = 20,
                idDirect = 60,
                idCur = 0,
                inPrice = 1,
                idTerritory = 1,
                fnotWithoutGTD = 0
            });
            
            Application.Run(new Form1(allClients));
        }
    }
}
