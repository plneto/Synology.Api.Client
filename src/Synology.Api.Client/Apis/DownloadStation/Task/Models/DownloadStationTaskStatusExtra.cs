using Newtonsoft.Json;

namespace Synology.Api.Client.Apis.DownloadStation.Task.Models
{
    public class DownloadStationTaskStatusExtra
    {
        [JsonProperty("error_detail")]
        public string ErrorDetail { get; set; }
    }
}
