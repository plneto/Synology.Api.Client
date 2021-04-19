namespace Synology.Api.Client.Apis.FileStation.Upload.Models
{
    public class FileStationUploadResponse
    {
        public bool BlSkip { get; set; }

        public string File { get; set; }

        public int Pid { get; set; }

        public int Progress { get; set; }
    }
}
