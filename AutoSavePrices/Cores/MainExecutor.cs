using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace AutoSavePrices
{
    public class MainExecutor
    {
        public List<Task> PoolTasks = new List<Task>();

        // Формирует задачу по клиенту
        public void GenerateTask(Client client)
        {
            Task newTask = new Task(() =>
            {
                try
                {
                    
                    Stopwatch startTime = Stopwatch.StartNew();

                    DataTable dt = DBExecutor.getDataTable(client);
                    
                    var exp = new ExportToExcel(dt);

                    Random rand = new Random();

                    string nameFile = client.idKontr + " - " + client.idKategory + " - " + rand.Next();

                    bool isExported = exp.StartExport(nameFile);

                    startTime.Stop();
                    if(isExported)
                        UniLogger.WriteLog($"Экспорт прайсов клиенту {client.idKontr} успешно завершен за ", 0, elapsedTime(startTime));

                    // Добавление пути к файлу
                }
                catch (Exception ex)
                {
                    UniLogger.WriteLog("Выполнение таска", 1, ex.Message);
                }
            });
            
            PoolTasks.Add(newTask);
            newTask.Start();
        }


        // Формируем потоки первых клиентов
        public void Start(Client[] firstClients)
        {
            foreach(var client in firstClients)
            {
                GenerateTask(client);
                Thread.Sleep(200);
            }
        }

        

        private string elapsedTime(Stopwatch time)
        {
            return String.Format("{0} мин | {1} сек",
                            time.Elapsed.Minutes,
                            time.Elapsed.Seconds);
        }

        

    }
}
