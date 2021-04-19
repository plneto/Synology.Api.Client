using System.Collections.Generic;

namespace Synology.Api.Client.Apis.FileStation.Extract.Models
{
    public class FileStationExtractListResponse
    {
        public IEnumerable<FileStationExtractListItem> Items { get; set; }

        public int Total { get; set; }
    }
}
