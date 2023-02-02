using TXSTBXRD_MIDDLEWARE.IDENTITY;
using System.Text.Json;
using System.Text;
using TXSTBXRD_UI.Utils;

namespace TXSTBXRD_UI.Services
{
    public class IdentityService : IDisposable
    {
        public HttpInterceptorService Interceptor { get; set; }
        public HttpClient _httpClient;

        public IdentityService(HttpClient client, HttpInterceptorService interceptor)
        {
            _httpClient = client;
            Interceptor = interceptor;
        }

        public async Task<string?> Authentication(LogIn loginData)
        {
            Interceptor.RegisterEvent();
            string json = JsonSerializer.Serialize<LogIn>(loginData);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var response = await _httpClient.PostAsync("identification", content);
                response.EnsureSuccessStatusCode();
                using var responseContent = await response.Content.ReadAsStreamAsync();
                return await JsonSerializer.DeserializeAsync<string>(responseContent);
            }
            catch (HttpRequestException)
            {
                return null;
            }
        }

        public async Task<bool> AddUser(Create newUser)
        {
            Interceptor.RegisterEvent();
            string json = JsonSerializer.Serialize<Create>(newUser);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("registration", content);
            response.EnsureSuccessStatusCode();

            using var responseContent = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<bool>(responseContent);
        }

        public async Task<UserDetails> GetDetails(UserToken userToken)
        {
            Interceptor.RegisterEvent();
            string json = JsonSerializer.Serialize<UserToken>(userToken);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("tokendetails", content);
            response.EnsureSuccessStatusCode();

            using var responseContent = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<UserDetails>(responseContent);
        }

        public void Dispose() => Interceptor.DisposeEvent();
    }
}