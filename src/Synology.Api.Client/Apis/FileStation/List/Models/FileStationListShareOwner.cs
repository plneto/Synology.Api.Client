using System;

namespace Synology.Api.Client.Apis.FileStation.List.Models
{
    public class FileStationListShareOwner
    {
        public uint Gid { get; set; }

        public string Group { get; set; }

        public uint Uid { get; set; }

        public string User { get; set; }
    }
}
