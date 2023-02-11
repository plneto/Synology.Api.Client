using System.Text.Json.Serialization;

namespace Synology.Api.Client.Apis.DownloadStation.Task.Models
{
    public class DownloadStationTaskDetail
    {
        [JsonPropertyName("completed_time")]
        public long CompletedTime { get; set; }

        [JsonPropertyName("connected_leechers")]
        public long ConnectedLeechers { get; set; }

        [JsonPropertyName("connected_peers")]
        public long ConnectedPeers { get; set; }

        [JsonPropertyName("connected_seeders")]
        public long ConnectedSeeders { get; set; }

        [JsonPropertyName("create_time")]
        public long CreateTime { get; set; }

        public string Destination { get; set; }

        [JsonPropertyName("seedelapsed")]
        public long SeedElapsed { get; set; }

        [JsonPropertyName("started_time")]
        public long StartedTime { get; set; }

        [JsonPropertyName("total_peers")]
        public long TotalPeers { get; set; }

        [JsonPropertyName("total_pieces")]
        public long TotalPieces { get; set; }

        [JsonPropertyName("unzip_password")]
        public string UnzipPassword { get; set; }

        public string Uri { get; set; }

        public string Priority { get; set; }

        [JsonPropertyName("waiting_seconds")]
        public long WaitingSeconds { get; set; }
    }
}
