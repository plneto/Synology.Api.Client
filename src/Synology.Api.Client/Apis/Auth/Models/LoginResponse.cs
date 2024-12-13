using System.Text.Json.Serialization;

namespace Synology.Api.Client.Apis.Auth.Models
{
    public class LoginResponse
    {
        [JsonPropertyName("is_portal_port")]
        public bool IsPortalPort { get; set; }

        public string Sid { get; set; } = null!;
    }
}
