using Newtonsoft.Json;

namespace Synology.Api.Client.Apis.FileStation.Search.Models
{
    public class FileStationSearchStartResponse
    {
        [JsonProperty("taskid")]
        public string TaskId { get; set; }
    }
}