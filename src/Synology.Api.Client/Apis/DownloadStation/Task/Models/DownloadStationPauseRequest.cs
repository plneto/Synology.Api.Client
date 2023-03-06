using System.Collections.Generic;

namespace Synology.Api.Client.Apis.DownloadStation.Task.Models
{
    public class DownloadStationPauseRequest
    {
        /// <summary>
        /// Task IDs to be paused in container
        /// </summary>
        public IEnumerable<string> Ids { get; set; }
    }
}