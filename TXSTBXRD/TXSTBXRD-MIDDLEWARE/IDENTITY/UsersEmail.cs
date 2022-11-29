using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


namespace TXSTBXRD_MIDDLEWARE.IDENTITY
{
    public class UsersEmail
    {
        [Required]
        [EmailAddress]
        [JsonPropertyName("email")]
        public string? Email { get; set; }
    }
}