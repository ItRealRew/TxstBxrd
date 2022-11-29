using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TXSTBXRD_MIDDLEWARE.IDENTITY
{
    public class UserDetails
    {
        [Required]
        [JsonPropertyName("fname")]
        public string? FirstName { get; set; }

        [JsonPropertyName("lname")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "The Email field is not a valid e-mail address.")]
        [EmailAddress]
        [JsonPropertyName("email")]
        public string? Email { get; set; }
    }
}