using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TXSTBXRD_MIDDLEWARE.IDENTITY
{
    public class LogIn
    {
        [Required]
        [JsonPropertyName("login")]
        public string? Login { get; set; }

        [Required]
        [JsonPropertyName("password")]
        public string? Password { get; set; }
    }
}