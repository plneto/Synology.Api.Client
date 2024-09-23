using System.Text.Json.Serialization;

namespace Synology.Api.Client.Apis.DownloadStation.Task.Models
{
    public class DownloadStationTaskTransfer
    {
        [JsonPropertyName("downloaded_pieces")]
        public long DownloadedPieces { get; set; }
        [JsonPropertyName("size_downloaded")]
        public long SizeDownloaded { get; set; }
        [JsonPropertyName("size_uploaded")]
        public long SizeUploaded { get; set; }
        [JsonPropertyName("speed_download")]
        public long SpeedDownload { get; set; }
        [JsonPropertyName("speed_upload")]
        public long SpeedUpload { get; set; }
    }

}
