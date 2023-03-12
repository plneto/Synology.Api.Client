using System.Text.Json.Serialization;

namespace Synology.Api.Client.Apis.DownloadStation.Info.Models
{
    public class DownloadStationInfoResponse
    {
        [JsonPropertyName("is_manager")] 
        public bool IsManager;
        
        [JsonPropertyName("version")] 
        public int Version;

        [JsonPropertyName("version_string")] 
        public string VersionString;
    }
}