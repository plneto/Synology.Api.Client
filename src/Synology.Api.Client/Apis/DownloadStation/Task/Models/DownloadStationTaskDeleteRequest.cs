using System.Collections.Generic;

namespace Synology.Api.Client.Apis.DownloadStation.Task.Models
{
    public class DownloadStationTaskDeleteRequest
    {
        /// <summary>
        /// Task IDs to be deleted in container
        /// </summary>
        public IEnumerable<string> Ids { get; set; }
        
        /// <summary>
        /// Delete tasks and force to move uncompleted download files to the destination.
        /// </summary>
        public bool ForceComplete { get; set; }
    }
}