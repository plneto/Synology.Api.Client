using Newtonsoft.Json;

namespace Synology.Api.Client.Apis.DownloadStation.Info.Models
{
    public class DownloadStationInfoResponse
    {
        [JsonProperty("is_manager")] 
        public string IsManager;
        
        [JsonProperty("version")] 
        public string Version;

        [JsonProperty("version_string")] 
        public string VersionString;
    }
}