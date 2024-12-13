using System.Text.Json.Serialization;

namespace Synology.Api.Client.Apis.FileStation.Search.Models
{
    public class FileStationSearchStartResponse
    {
        [JsonPropertyName("taskid")]
        public string? TaskId { get; set; }
    }
}