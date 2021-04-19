using Newtonsoft.Json;

namespace Synology.Api.Client.Apis.FileStation.List.Models
{
    public class FileStationListShare
    {
        [JsonProperty("isdir")]
        public bool IsDir { get; set; }

        public string Name { get; set; }

        public string Path { get; set; }

        public FileStationListShareAdditional Additional { get; set; }
    }
}
