using TXSTBXRD_MIDDLEWARE.COMMUNICATIONS;
using TXSTBXRD_MIDDLEWARE.IDENTITY;
using System.Text.Json;
using System.Text;

namespace COMMUNICATIONS_SERVICE.Services
{
    public class EmailService
    {
        public HttpClient _httpClient;

        public EmailService(HttpClient client)
        {
            _httpClient = client;
        }
        public async Task<bool> ResetUserPassword(PasswordRecovery dateReset)
        {
            var details = await IsValidUserEmail(dateReset);

            if (details?.Email is null)
                return false;
            
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

        public void SendMessage(){
            
        }
    }
}