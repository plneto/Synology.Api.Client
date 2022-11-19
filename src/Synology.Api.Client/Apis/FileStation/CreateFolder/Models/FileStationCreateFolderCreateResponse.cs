using System.Collections.Generic;
using Newtonsoft.Json;
using Synology.Api.Client.Apis.FileStation.List.Models;

namespace Synology.Api.Client.Apis.FileStation.CreateFolder.Models
{
    public class FileStationCreateFolderCreateResponse
    {
        public IEnumerable<FileStationListShare> Folders { get; set; }
    }
}
