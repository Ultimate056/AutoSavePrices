using System;
using System.Text;
using System.Threading.Tasks;
using MimeKit;
using MailKit.Net.Smtp;
using AutoSavePrices.Models;
using System.IO;

namespace AutoSavePrices.Services
{
    public class SendMailExecutor
    {
        private readonly string from = "Supplier@arkona36.ru";
        private readonly string password = "sppu54ffk)";

        private const string mailToMy = "kopylovvu@arkona36.ru";

        public SendMailExecutor()
        {
            
        }


        public bool SendEmailAsync(RoutePrice path_price, string MailTo = mailToMy)
        {
            MimeMessage emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Компания Аркона", from));
            emailMessage.To.Add(new MailboxAddress("", MailTo));
            emailMessage.Subject = "Прайс листов";
            var builder = new BodyBuilder();
            builder.HtmlBody = $"<h2 style='color: green'>Вам для категории {path_price.Category} ? </h2></br>" +
                $"Держите прайсы, уважаемый {path_price.IdClient} клиент";

            builder.Attachments.Add(path_price.FullPath);
            emailMessage.Body = builder.ToMessageBody();

            try
            {
                using (var client = new SmtpClient())
                {
                    client.Connect("mail.arkona36.ru", 587, false);
                    client.Authenticate(from, password);
                    client.Send(emailMessage);

                    client.Disconnect(true);
                }
                if (Directory.Exists(path_price.PathDirectory))
                {
                    Directory.Delete(path_price.PathDirectory, true);
                }
                return true;
            }
            catch(Exception ex)
            {
                UniLogger.WriteLog("Ошибка при отправке почты ", 1, ex.Message);
                return false;
            }

        }
    }
}
