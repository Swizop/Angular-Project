using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ConAPI.Services.EmailServices
{
    public class EmailService : IEmailService
    {
        public void SendEmail(string address, string projectTitle, string phone, string email, string name)
        {
            string to = address; //To address    
            string from = "test.conncon@gmail.com";
            MailMessage message = new MailMessage(from, to);

            string mailbody = "Bună ziua,\n\nUtilizatorul " + name + " v-a acceptat oferta pentru proiectul '" + projectTitle + "'. Acesta poate fi contactat la numărul de telefon: " + phone + " sau la adresa de email " + email;
            message.Subject = "Connect & Construct: Acceptare ofertă";
            message.Body = mailbody;
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = false;
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            System.Net.NetworkCredential basicCredential1 = new
            System.Net.NetworkCredential("test.conncon@gmail.com", "Conncon1!");
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = basicCredential1;
            try
            {
                client.Send(message);
            }

            catch (Exception ex)
            {
                // do nothing
            }
            return;
        }
    }
}
