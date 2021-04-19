using Newtonsoft.Json;

namespace Synology.Api.Client.Apis.Auth.Models
{
    public class LoginResponse
    {
        [JsonProperty("is_portal_port")]
        public bool IsPortalPort { get; set; }

        public string Sid { get; set; }
    }
}
