using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TXSTBXRD_MIDDLEWARE.NOTIFICATIONS
{
    public class PasswordRecovery
    {
        [Required]
        [JsonPropertyName("email")]
        [EmailAddress]
        public string? email { get; set; }
    }
}