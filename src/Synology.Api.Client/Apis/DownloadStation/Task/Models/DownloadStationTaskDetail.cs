using Newtonsoft.Json;

namespace Synology.Api.Client.Apis.DownloadStation.Task.Models
{
    public class DownloadStationTaskDetail
    {
        [JsonProperty("completed_time")]
        public long CompletedTime { get; set; }

        [JsonProperty("connected_leechers")]
        public long ConnectedLeechers { get; set; }

        [JsonProperty("connected_peers")]
        public long ConnectedPeers { get; set; }

        [JsonProperty("connected_seeders")]
        public long ConnectedSeeders { get; set; }

        [JsonProperty("create_time")]
        public long CreateTime { get; set; }

        public string Destination { get; set; }

        [JsonProperty("seedelapsed")]
        public long SeedElapsed { get; set; }

        [JsonProperty("started_time")]
        public long StartedTime { get; set; }

        [JsonProperty("total_peers")]
        public long TotalPeers { get; set; }

        [JsonProperty("total_pieces")]
        public long TotalPieces { get; set; }

        [JsonProperty("unzip_password")]
        public string UnzipPassword { get; set; }

        public string Uri { get; set; }

        public string Priority { get; set; }

        [JsonProperty("waiting_seconds")]
        public long WaitingSeconds { get; set; }
    }
}
