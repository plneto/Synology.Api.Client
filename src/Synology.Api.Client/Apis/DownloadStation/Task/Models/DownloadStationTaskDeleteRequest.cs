using Newtonsoft.Json;

namespace Synology.Api.Client.Apis.DownloadStation.Task.Models
{
    public class DownloadStationTaskDeleteRequest
    {
        public string Id { get; set; }
        
        [JsonProperty("force_complete")]
        public bool ForceComplete { get; set; }
    }
}