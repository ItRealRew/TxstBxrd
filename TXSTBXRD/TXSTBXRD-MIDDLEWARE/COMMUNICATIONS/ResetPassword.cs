using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TXSTBXRD_MIDDLEWARE.COMMUNICATIONS
{
    public class ResetPassword
    {
        [Required]
        [JsonPropertyName("email")]
        public string? Email { get; set; }
    }
}