using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Synology.Api.Client.Apis.DownloadStation.Task.Models
{
    public class DownloadStationTaskCreateResponse
    {
        [JsonPropertyName("task_id")]
        public IEnumerable<string>? TaskId { get; set; }
    }
}
