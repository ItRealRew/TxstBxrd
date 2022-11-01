using TXSTBXRD_MIDDLEWARE.IDENTITY;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Text;

namespace TXSTBXRD_UI.Services
{
    public class IdentityService
    {
        public HttpClient _httpClient;

        public IdentityService(HttpClient client)
        {
            _httpClient = client;
        }

        public async Task<Guid> Authentication()
        {
            var login = new LogIn { Login = "root1", Password = "root1" };
            string json = JsonSerializer.Serialize<LogIn>(login);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("identification", content);
            response.EnsureSuccessStatusCode();

            using var responseContent = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<Guid>(responseContent);
        }
    }
}