using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace AutoSavePrices.Services
{
    public class SendMailExecutor
    {
        private readonly MailAddress from = new MailAddress("Supplier@arkona36.ru");
        private readonly string password = "sppu54ffk)";

        private const string mailToMy = "kopylov36@arkona.ru";

        private SmtpClient smtp;

        public SendMailExecutor()
        {
            
        }

        public void InitializeSmtpConf()
        {
            smtp.Host = "mail.arkona36.ru";
            smtp.Port = 587;
            smtp.Credentials = new NetworkCredential(from.ToString(), password);
            smtp.EnableSsl = false;
        }


        public bool StartSendMails(string path, decimal idClient, string mailTo = mailToMy)
        {
            try
            {
                smtp = new SmtpClient();
                InitializeSmtpConf();


                MailAddress to = new MailAddress(mailTo);
                MailMessage m = new MailMessage(from, to);

                m.Subject = $"Лист прайсов для {idClient}";
                Attachment att = new Attachment(path);
                m.Attachments.Add(att);
                m.Body = $"Лист прайсов для вас, уважаемый {idClient}";

                smtp.Send(m);

                m.Dispose();

                return true;
            }
            catch (Exception ex)
            {
                UniLogger.WriteLog("Ошибка при отправке почты ", 1, ex.Message);

                return false;
            }
        }

    }
}
