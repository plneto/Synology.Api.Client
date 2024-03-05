using System;

namespace Synology.Api.Client.Apis.FileStation.List.Models
{
    public class FileStationListShareOwner
    {
        public long Gid { get; set; }

        public string Group { get; set; }

        public long Uid { get; set; }

        public string User { get; set; }
    }
}
