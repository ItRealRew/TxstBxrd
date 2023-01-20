using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TXSTBXRD_MIDDLEWARE.IDENTITY
{
    public class UserToken
    {
        [Required]
        [JsonPropertyName("authorizationToken")]
        public Guid authorizationToken { get; set; }
    }
}