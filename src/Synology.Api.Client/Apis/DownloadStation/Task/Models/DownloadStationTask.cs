using System.Text.Json.Serialization;

namespace Synology.Api.Client.Apis.DownloadStation.Task.Models
{
    public class DownloadStationTask
    {
        public string Id { get; set; }

        public string Type { get; set; }

        public string Username { get; set; }

        public string Title { get; set; }

        public decimal Size { get; set; }

        public string Status { get; set; }

        [JsonPropertyName("status_extra")]
        public DownloadStationTaskStatusExtra StatusExtra { get; set; }

        public DownloadStationTaskAdditional Additional { get; set; }
    }
}
