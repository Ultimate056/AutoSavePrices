using AutoSavePrices.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoSavePrices
{
    static class Program
    {

        private static readonly int maxClientsInThreads = 4;

        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            QAllClients allClients = new QAllClients(maxClientsInThreads);

            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                //string sql = @"SELECT * FROM uf_getClientsPrices (@id_u)";
                //SqlParameter par = new SqlParameter("@id_u", id_agent);
                //var list = db.Database.SqlQuery<Client>(sql).ToList();

                var list = db.a_kontrs.Where(x => x.category == 4 || x.category == 21 ||
                x.category == 31 || x.category == 20)
                    .Take(100)
                    .Select(x => new Client
                    {
                        idKontr = x.id_kontr,
                        idKategory = x.category,
                        idDirect = 60,
                        idCur = 0,
                        inPrice = 1,
                        idTerritory = 1
                    }).ToList();

                // Для теста
                var end_list = list.Take(4);

                foreach(var item in end_list)
                {
                    allClients.Add(item);
                }
            }
            
            Application.Run(new Form1(allClients));
        }
    }
}
