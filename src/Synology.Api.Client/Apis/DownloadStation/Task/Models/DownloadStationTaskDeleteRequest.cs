using System.Collections.Generic;
using Newtonsoft.Json;

namespace Synology.Api.Client.Apis.DownloadStation.Task.Models
{
    public class DownloadStationTaskDeleteRequest
    {
        public IEnumerable<string> Ids { get; set; }
        
        public bool ForceComplete { get; set; }
    }
}