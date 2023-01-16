using Newtonsoft.Json;

namespace Synology.Api.Client.Apis.DownloadStation.Info.Models
{
    public class DownloadStationGetConfigResponse
    {
        [JsonProperty("bt_max_download")] public int BtMaxDownloadSpeed;
        [JsonProperty("bt_max_upload")] public int BtMaxUploadSpeed;
        [JsonProperty("emule_enabled")] public bool EmulEnable;
        [JsonProperty("emul_max_download")] public int EmulMaxDownloadSpeed;
        [JsonProperty("emul_max_upload")] public int EmulMaxUploadSpeed;
        [JsonProperty("ftp_max_download")] public int FtpMaxDownloadSpeed;
        [JsonProperty("http_max_download")] public int HttpMaxDownloadSpeed;
        [JsonProperty("nzb_max_download")] public int NzbMaxDownloadSpeed;
        [JsonProperty("unzip_service_enabled")] public bool UnzipServiceEnable;
        [JsonProperty("default_destination")] public string DefaultDestination;
        [JsonProperty("emul_default_destination")] public string EmulDefaultDestination;
    }
}