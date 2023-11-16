using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Calcul
{
    public interface IMailSender
    {
        void SendEmail(string recipient, string subject, string body);
    }

    public class MockMailSender : IMailSender
    {
        public void SendEmail(string recipient, string subject, string body)
        {
            // Вместо фактической отправки электронной почты, мы просто имитируем ее здесь
            Console.WriteLine($"Отправка электронной почты:\nПолучатель: {recipient}\nТема: {subject}\nТело: {body}");
        }
    }

    public class EmailSender : IMailSender
    {
        public void SendEmail(string recipient, string subject, string body)
        {
            // Адрес отправителя и SMTP-сервер
            string senderEmail = "your-email@example.com";
            string smtpServer = "smtp.example.com";
            int smtpPort = 587; // Порт SMTP-сервера

            // Логин и пароль для авторизации на SMTP-сервере
            string username = "your-username";
            string password = "your-password";

            try
            {
                // Создание объекта MailMessage для формирования письма
                MailMessage mail = new MailMessage(senderEmail, recipient, subject, body);

                // Создание объекта SmtpClient для отправки письма через SMTP-сервер
                SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort);
                smtpClient.Credentials = new System.Net.NetworkCredential(username, password);
                smtpClient.EnableSsl = true; // Использовать SSL-шифрование

                // Отправка письма
                smtpClient.Send(mail);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при отправке письма: " + ex.Message);
            }
        }
    }
}