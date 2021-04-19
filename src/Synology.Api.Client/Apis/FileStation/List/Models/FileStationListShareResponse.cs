using System.Collections.Generic;

namespace Synology.Api.Client.Apis.FileStation.List.Models
{
    public class FileStationListShareResponse
    {
        public int Total { get; set; }

        public int Offset { get; set; }

        public IEnumerable<FileStationListShare> Shares { get; set; }
    }
}
