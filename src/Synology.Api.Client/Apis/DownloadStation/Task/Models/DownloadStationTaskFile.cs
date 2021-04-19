using Newtonsoft.Json;

namespace Synology.Api.Client.Apis.DownloadStation.Task.Models
{
    public class DownloadStationTaskFile
    {
        public string Filename { get; set; }

        public string Priority { get; set; }

        public decimal Size { get; set; }

        [JsonProperty("size_downloaded")]
        public decimal SizeDownloaded { get; set; }
    }
}
