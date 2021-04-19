using Newtonsoft.Json;

namespace Synology.Api.Client.Apis.FileStation.List.Models
{
    public class FileStationListShareAdditional
    {
        [JsonProperty("real_path")]
        public string RealPath { get; set; }

        public FileStationListShareOwner Owner { get; set; }

        public FileStationListShareTime Time { get; set; }

        public decimal Size { get; set; }
    }
}
