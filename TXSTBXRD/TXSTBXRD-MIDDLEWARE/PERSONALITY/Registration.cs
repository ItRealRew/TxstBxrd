using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TXSTBXRD_MIDDLEWARE.PERSONALITY
{
    public class Registration
    {
        [Required]
        [JsonPropertyName("fname")]
        public string? FirstName { get; set; }

        [JsonPropertyName("lname")]
        public string? LastName { get; set; }
        
        [Required(ErrorMessage = "Username should be from 3 to 50 length")]
        [StringLength(50, MinimumLength = 3)]
        [JsonPropertyName("login")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "The Email field is not a valid e-mail address.")]
        [EmailAddress]
        [JsonPropertyName("email")]
        public string? Email { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$", ErrorMessage = "Password must be between 8 and 15 characters and contain one uppercase letter, one lowercase letter, one digit and one special character.")]
        [JsonPropertyName("password")]
        public string? Password { get; set; }        
    }
}