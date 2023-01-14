using Newtonsoft.Json;

namespace Synology.Api.Client.Apis.DownloadStation.Info.Models
{
    public class DownloadStationInfoResponse
    {
        [JsonProperty("is_manager")] 
        public bool IsManager;
        
        [JsonProperty("version")] 
        public int Version;

        [JsonProperty("version_string")] 
        public string VersionString;
    }
}