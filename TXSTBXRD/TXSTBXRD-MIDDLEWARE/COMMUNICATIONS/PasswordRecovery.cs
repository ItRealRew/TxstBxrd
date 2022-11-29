using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TXSTBXRD_MIDDLEWARE.COMMUNICATIONS
{
    public class PasswordRecovery
    {
        [Required]
        [EmailAddress]
        [JsonPropertyName("email")]
        public string? Email { get; set; }
    }
}