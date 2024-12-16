using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Synology.Api.Client.Apis.DownloadStation.Task.Models;

public class DownloadStationTaskDeleteResponse
{
    [JsonPropertyName("failed_task")]
    public IEnumerable<DownloadStationTaskFailedTaskResponse> FailedTask { get; set; }
}