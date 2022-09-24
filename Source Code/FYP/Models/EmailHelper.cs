using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace FYP.Models
{
    public static class EmailHelper
    {
        public static void SendEmail(string email, string subject, string body) {
            MailMessage message = new MailMessage();
            message.To.Add(email);
            message.From = new MailAddress(EmailSetting.From);
            message.Subject = subject;
            message.Body = body;

            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            SmtpServer.Port = 587;
            SmtpServer.UseDefaultCredentials = false;
            SmtpServer.Credentials = new System.Net.NetworkCredential(EmailSetting.From, EmailSetting.Password);
            SmtpServer.EnableSsl = true;
            SmtpServer.Send(message);
        }
    }
}