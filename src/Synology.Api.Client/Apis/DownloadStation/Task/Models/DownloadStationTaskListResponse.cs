using System.Collections.Generic;

namespace Synology.Api.Client.Apis.DownloadStation.Task.Models
{
    public class DownloadStationTaskListResponse
    {
        public int Total { get; set; }

        public int Offset { get; set; }

        public IEnumerable<DownloadStationTask> Tasks { get; set; }
    }
}
