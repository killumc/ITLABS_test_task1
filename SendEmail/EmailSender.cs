using Poll_ver2.MVVM.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Poll_ver2.SendEmail
{
    public class EmailSender
    {
        public static SmtpSettings LoadSettings(string path)
        {
            try
            {
                var json = File.ReadAllText(path);
                var settings = JsonSerializer.Deserialize<SmtpSettings>(json);
                return settings;
            }

            catch (Exception ex)
            {
                throw new InvalidOperationException("Ошибка загрузки настроек из файла", ex);
            }
        }

        public void SendMessage(string toEmail, string subject, string body)
        {
            var smtpSettings = LoadSettings("emailSettings.json"); // путь к файлу с настройками

            using (var smtpClient = new SmtpClient(smtpSettings.SmtpServer, smtpSettings.SmtpPort))
            {
                smtpClient.Credentials = new NetworkCredential(smtpSettings.SmtpUsername, smtpSettings.SmtpPassword);
                smtpClient.EnableSsl = true;

                using (var mailMessage = new MailMessage())
                {
                    mailMessage.From = new MailAddress(smtpSettings.SmtpUsername);
                    mailMessage.To.Add(toEmail);
                    mailMessage.Subject = subject;
                    mailMessage.Body = body;

                    smtpClient.Send(mailMessage);
                }
            }
        }
    }
}
