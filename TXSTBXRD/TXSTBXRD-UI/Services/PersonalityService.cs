using TXSTBXRD_MIDDLEWARE.PERSONALITY;
using TXSTBXRD_UI.Utils;
using System.Text.Json;
using System.Text;

namespace TXSTBXRD_UI.Services
{
    public class PersonalityService : IDisposable
    {
        public HttpInterceptorService Interceptor { get; set; }
        public HttpClient _httpClient;
        
        public PersonalityService(HttpClient client, HttpInterceptorService interceptor)
        {
            _httpClient = client;
            Interceptor = interceptor;
        }

        public async Task<bool> UserRegistration(Registration newUser)
        {
            Interceptor.RegisterEvent();
            string json = JsonSerializer.Serialize<Registration>(newUser);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("registration", content);
            response.EnsureSuccessStatusCode();

            using var responseContent = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<bool>(responseContent);
        }

        public void Dispose() => Interceptor.DisposeEvent();
    }
}