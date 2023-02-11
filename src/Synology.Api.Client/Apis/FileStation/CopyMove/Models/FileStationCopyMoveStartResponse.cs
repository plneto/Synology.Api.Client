using System.Text.Json.Serialization;

namespace Synology.Api.Client.Apis.FileStation.CopyMove.Models
{
    public class FileStationCopyMoveStartResponse
    {
        [JsonPropertyName("taskid")]
        public string TaskId { get; set; }
    }
}
