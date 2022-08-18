using Demo.DAL.Entities;
using System.Net;
using System.Net.Mail;

namespace Demo.PL.Helper
{
    public static class EmailSettings
    {
        public static void SendEmail(Email email)
        {
            var Client = new SmtpClient("stmp.sendgrid.net", 587);
            Client.EnableSsl = true;
            Client.Credentials = new NetworkCredential("apikey", "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");
            Client.Send("amednaser975@gmail.com", "a.azmy93@gmail.com", email.Title, email.Body);
        }
    }
}
