using Newtonsoft.Json;

namespace Synology.Api.Client.Apis.FileStation.Extract.Models
{
    public class FileStationExtractListItem
    {
        [JsonProperty("item_id")]
        public int ItemId { get; set; }

        public string Name { get; set; }

        public decimal Size { get; set; }

        [JsonProperty("pack_size")]
        public decimal PackSize { get; set; }

        public string Mtime { get; set; }

        public string Path { get; set; }

        [JsonProperty("is_dir")]
        public bool IsDir { get; set; }
    }
}
