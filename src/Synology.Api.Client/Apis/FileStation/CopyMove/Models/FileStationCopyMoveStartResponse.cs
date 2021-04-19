using Newtonsoft.Json;

namespace Synology.Api.Client.Apis.FileStation.CopyMove.Models
{
    public class FileStationCopyMoveStartResponse
    {
        [JsonProperty("taskid")]
        public string TaskId { get; set; }
    }
}
