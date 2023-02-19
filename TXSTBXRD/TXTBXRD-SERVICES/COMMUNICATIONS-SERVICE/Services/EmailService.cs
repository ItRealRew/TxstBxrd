using TXSTBXRD_MIDDLEWARE.COMMUNICATIONS;
using TXSTBXRD_MIDDLEWARE.IDENTITY;
using System.Text.Json;
using System.Text;
using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using TXSTBXD_LIBS.Email;

namespace COMMUNICATIONS_SERVICE.Services
{
    public class EmailService
    {
        public HttpClient _httpClient;

        public TXSTBXD_LIBS.Email.EmailService email;

        public EmailService(HttpClient client, TXSTBXD_LIBS.Email.EmailService mail)
        {
            _httpClient = client;
            email = mail;
        }
        public async Task<bool> ResetUserPassword(PasswordRecovery dateReset)
        {
            var details = await IsValidUserEmail(dateReset);

            if (details?.Email is null)
                return false;

            //SendMessage(details.Email);
            email.SendMessageNoReply(details.Email, "Тест", "<i><b>Тест</b><i>");
            return true;
        }

        public async Task<UserDetails?> IsValidUserEmail(PasswordRecovery userMail)
        {
            string json = JsonSerializer.Serialize<PasswordRecovery>(userMail);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("emaildetails", content);
            response.EnsureSuccessStatusCode();

            using var responseContent = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<UserDetails>(responseContent);
        }

        public void SendMessage1(string userMail)
        {
            var MyConfig = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            MailMessage mail = new MailMessage();

            var From = MyConfig.GetValue<string>("EmailSettings:UserName");
            var Host = MyConfig.GetValue<string>("EmailSettings:SmtpHost");
            var Port = MyConfig.GetValue<int>("EmailSettings:SmtpPort");
            var UserName = MyConfig.GetValue<string>("EmailSettings:UserName"); 
            var Password = MyConfig.GetValue<string>("EmailSettings:Password"); 

            mail.From = new MailAddress(From); // Адрес отправителя
            mail.To.Add(new MailAddress(userMail)); // Адрес получателя
            mail.Subject = "Заголовок";
            mail.Body = "Письмо........................";
            SmtpClient client = new SmtpClient();
            client.Host = Host;
            client.Port =  Port;
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(UserName, Password);
            client.Send(mail);
        }
    }
}