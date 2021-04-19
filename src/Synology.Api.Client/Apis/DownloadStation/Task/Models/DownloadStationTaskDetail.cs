using Newtonsoft.Json;

namespace Synology.Api.Client.Apis.DownloadStation.Task.Models
{
    public class DownloadStationTaskDetail
    {
        [JsonProperty("connected_leechers")]
        public int ConnectedLeechers { get; set; }

        [JsonProperty("connected_seeders")]
        public int ConnectedSeeders { get; set; }

        [JsonProperty("create_time")]
        public decimal CreateTime { get; set; }

        public string Destination { get; set; }

        public string Priority { get; set; }

        [JsonProperty("total_peers")]
        public int TotalPeers { get; set; }

        public string Uri { get; set; }
    }
}
