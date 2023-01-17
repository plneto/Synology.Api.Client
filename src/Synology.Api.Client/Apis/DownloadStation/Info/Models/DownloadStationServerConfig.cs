using Newtonsoft.Json;

namespace Synology.Api.Client.Apis.DownloadStation.Info.Models
{
    public class DownloadStationServerConfig
    {
        public DownloadStationServerConfig(
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

        [JsonProperty("bt_max_download")] public int? BtMaxDownloadSpeed { get; }
        [JsonProperty("bt_max_upload")] public int? BtMaxUploadSpeed { get; }
        [JsonProperty("emule_enabled")] public bool? EmulEnable { get; }
        [JsonProperty("emul_max_download")] public int? EmulMaxDownloadSpeed { get; }
        [JsonProperty("emul_max_upload")] public int? EmulMaxUploadSpeed { get; }
        [JsonProperty("ftp_max_download")] public int? FtpMaxDownloadSpeed { get; }
        [JsonProperty("http_max_download")] public int? HttpMaxDownloadSpeed { get; }
        [JsonProperty("nzb_max_download")] public int? NzbMaxDownloadSpeed { get; }
        [JsonProperty("unzip_service_enabled")] public bool? UnzipServiceEnable { get; }
        [JsonProperty("default_destination")] public string DefaultDestination { get; }
        [JsonProperty("emul_default_destination")] public string EmulDefaultDestination { get; }
    }
}