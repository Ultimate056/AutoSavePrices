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
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            QAllClients allClients = new QAllClients(4);

            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                //string sql = @"SELECT * FROM uf_getClientsPrices (@id_u)";
                //SqlParameter par = new SqlParameter("@id_u", id_agent);
                //var list = db.Database.SqlQuery<Client>(sql).ToList();
                var list = db.a_kontrs.Where(x=> x.category == 21).Take(100).Select(x => new Client
                {
                    idKontr = x.id_kontr,
                    idKategory = x.category,
                    idDirect = 60,
                    idCur = 0,
                    inPrice = 1,
                    idTerritory = 1
                }).ToList();
                foreach(var item in list)
                {
                    allClients.Add(item);
                }
            }
            
            Application.Run(new Form1(allClients));
        }
    }
}
