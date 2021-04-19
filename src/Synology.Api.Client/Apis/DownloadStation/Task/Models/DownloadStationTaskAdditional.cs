using System.Collections.Generic;

namespace Synology.Api.Client.Apis.DownloadStation.Task.Models
{
    public class DownloadStationTaskAdditional
    {
        public DownloadStationTaskDetail Detail { get; set; }

        public IEnumerable<DownloadStationTaskFile> File { get; set; }
    }
}
