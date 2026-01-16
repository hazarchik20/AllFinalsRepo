using System.Text.Json.Serialization;

namespace BLL.Models
{
    public class SmaleUser
    {
        [JsonPropertyName("email")]
        public string Email { get; set; }
        [JsonPropertyName("password")]

        public string Password { get; set; }
    }
}