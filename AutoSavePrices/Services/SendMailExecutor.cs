using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace AutoSavePrices.Services
{
    public static class SendMailExecutor
    {
        private static MailAddress from = new MailAddress("ugrass_056@mail.ru");
        private static string password = "E80qugzHAy0idJxd4ZwM";

        private static string mailTo = "kopylov36@arkona.ru";

        public static bool StartSendMails(string path, decimal idClient)
        {
            //var task = Task.Run(() =>
            //{
            //    SmtpClient smtp = new SmtpClient();
            //    try
            //    {
            //        smtp.Host = "smtp.mail.ru";
            //        smtp.Port = 465;
            //        smtp.Credentials = new NetworkCredential(from.ToString(), password);
            //        smtp.EnableSsl = false;

            //        MailAddress to = new MailAddress(mailTo);
            //        MailMessage m = new MailMessage(from, to);

            //        m.Subject = $"Лист прайсов для {idClient}";
            //        Attachment att = new Attachment(path);
            //        m.Attachments.Add(att);
            //        m.Body = $"Держи короч, {idClient}";

            //        smtp.Send(m);
            //    }
            //    catch (Exception ex)
            //    {
            //        UniLogger.WriteLog("Отправка сообщения на почту ", 1, ex.Message);

            //    }
            //    finally
            //    {
            //        smtp.Dispose();
            //    }
            //});
            return true;
        }

    }
}
