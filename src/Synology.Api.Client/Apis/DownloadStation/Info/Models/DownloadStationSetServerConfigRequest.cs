namespace Synology.Api.Client.Apis.DownloadStation.Info.Models
{
    public class DownloadStationSetServerConfigRequest
    {
        public DownloadStationSetServerConfigRequest(
            int? btMaxDownloadSpeed = null, 
            int? btMaxUploadSpeed = null, 
            bool? emulEnable = null, 
            int? emulMaxDownloadSpeed = null, 
            int? emulMaxUploadSpeed = null, 
            int? ftpMaxDownloadSpeed = null, 
            int? httpMaxDownloadSpeed = null, 
            int? nzbMaxDownloadSpeed = null, 
            bool? unzipServiceEnable = null, 
            string defaultDestination = null, 
            string emulDefaultDestination = null)
        {
            BtMaxDownloadSpeed = btMaxDownloadSpeed;
            BtMaxUploadSpeed = btMaxUploadSpeed;
            EmulEnable = emulEnable;
            EmulMaxDownloadSpeed = emulMaxDownloadSpeed;
            EmulMaxUploadSpeed = emulMaxUploadSpeed;
            FtpMaxDownloadSpeed = ftpMaxDownloadSpeed;
            HttpMaxDownloadSpeed = httpMaxDownloadSpeed;
            NzbMaxDownloadSpeed = nzbMaxDownloadSpeed;
            UnzipServiceEnable = unzipServiceEnable;
            DefaultDestination = defaultDestination;
            EmulDefaultDestination = emulDefaultDestination;
        }

        public int? BtMaxDownloadSpeed { get; }
        public int? BtMaxUploadSpeed { get; }
        public bool? EmulEnable { get; }
        public int? EmulMaxDownloadSpeed { get; }
        public int? EmulMaxUploadSpeed { get; }
        public int? FtpMaxDownloadSpeed { get; }
        public int? HttpMaxDownloadSpeed { get; }
        public int? NzbMaxDownloadSpeed { get; }
        public bool? UnzipServiceEnable { get; }
        public string DefaultDestination { get; }
        public string EmulDefaultDestination { get; }
    }
}