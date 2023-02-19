using System.Net;
using System.Net.Mail;
using TXSTBXRD_LIBS.Email.Types;

namespace TXSTBXD_LIBS.Email
{
    public class EmailService
    {
        public void SendMessageNoReply(string userMail, string subject, string htmlBody)
        {
            MailMessage mail = new MailMessage();

            mail.From = new MailAddress(CorporateEmail.NoReply.NameCorporateMail); 
            mail.To.Add(new MailAddress(userMail));

            mail.Subject = subject;
            mail.Body = htmlBody;
            mail.IsBodyHtml = true;
            
            SmtpClient client = new SmtpClient();

            client.Host = CorporateEmail.NoReply.Host;
            client.Port = CorporateEmail.NoReply.Port;
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(CorporateEmail.NoReply.NameCorporateMail, CorporateEmail.NoReply.Password);
            client.Send(mail);
        }
    }
}