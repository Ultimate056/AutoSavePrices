using AutoSavePrices.Configurations;
using AutoSavePrices.Models;
using AutoSavePrices.Services;
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
                    // Настройка путей для файла
                    RoutePrice route = new RoutePrice()
                    {
                        IdClient = client.idKontr.ToString(),
                        Category = client.idKategory.ToString(),
                        NameFile = "avtoprices"
                    };
                    route.FullPath = ConfExp.GetFullPath(route);


                    Stopwatch startTime = Stopwatch.StartNew();

                    // Получаем данные
                    DataTable dt = DBExecutor.getDataTable(client);
             
                    // Экспортируем в Excel
                    var exp = new ExportToExcel(dt);
                    bool isExported = exp.StartExport(route);
                    startTime.Stop();
                    if(isExported)
                        UniLogger.WriteLog($"Экспорт прайсов клиенту {client.idKontr} успешно завершен за ", 0, elapsedTime(startTime));
                    else
                        throw new Exception($"Ошибка при экспорте файла. Клиент {client.idKontr}. Категория {client.idKategory}");

                    // Отправка email
                    startTime.Start();
                    var sme = new SendMailExecutor();
                    bool isSended =  sme.SendEmailAsync(route);

                    startTime.Stop();
                    if (isSended)
                        UniLogger.WriteLog($"Отправились прайсы  {client.idKontr} за ", 0, elapsedTime(startTime));
                    else
                        throw new Exception($"Не Отправились прайсы  {client.idKontr}");
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
                Thread.Sleep(300);
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
