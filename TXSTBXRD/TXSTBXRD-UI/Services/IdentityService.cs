using TXSTBXRD_MIDDLEWARE.IDENTITY;
using TXSTBXRD_UI.Utils;
using System.Text.Json;
using System.Text;

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

        public async Task<UserDetails> GetDetails(UserToken userToken)
        {
            Interceptor.RegisterEvent();
            string json = JsonSerializer.Serialize<UserToken>(userToken);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("tokendetails", content);
            response.EnsureSuccessStatusCode();

            using var responseContent = await response.Content.ReadAsStreamAsync();

            UserDetails? orNull = await JsonSerializer.DeserializeAsync<UserDetails>(responseContent);
            return orNull ?? throw new JsonException( "Expected deserialized object to be non-null, but encountered null." );;
        }

        public void Dispose() => Interceptor.DisposeEvent();
    }
}