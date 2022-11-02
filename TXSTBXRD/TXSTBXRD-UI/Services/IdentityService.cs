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

        public async Task<string?> Authentication(LogIn loginData)
        {
            string json = JsonSerializer.Serialize<LogIn>(loginData);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("identification", content);
            response.EnsureSuccessStatusCode();

            using var responseContent = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<string>(responseContent);
        }
    }
}