using Newtonsoft.Json;

namespace Synology.Api.Client.Apis.FileStation.Extract.Models
{
    public class FileStationExtractStatusResponse
    {
        [JsonProperty("dest_folder_path")]
        public string DestFolderPath { get; set; }

        public bool Finished { get; set; }

        public decimal Progress { get; set; }
    }
}
