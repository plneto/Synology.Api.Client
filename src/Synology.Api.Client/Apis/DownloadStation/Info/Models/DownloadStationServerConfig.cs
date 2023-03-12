using System.Text.Json.Serialization;

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
            string? defaultDestination = null, 
            string? emulDefaultDestination = null)
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

        /// <summary>
        /// Max BT download speed in KB/s (“0” means unlimited)
        /// </summary>
        [JsonPropertyName("bt_max_download")] public int? BtMaxDownloadSpeed { get; }
        /// <summary>
        /// Max BT upload speed in KB/s (“0” means unlimited)
        /// </summary>
        [JsonPropertyName("bt_max_upload")] public int? BtMaxUploadSpeed { get; }
        /// <summary>
        /// If eMule service is enabled
        /// </summary>
        [JsonPropertyName("emule_enabled")] public bool? EmulEnable { get; }
        /// <summary>
        /// Max eMule download speed in KB/s (“0” means unlimited)
        /// </summary>
        [JsonPropertyName("emul_max_download")] public int? EmulMaxDownloadSpeed { get; }
        /// <summary>
        /// Max eMule upload speed in KB/s (“0” means unlimited)
        /// </summary>
        [JsonPropertyName("emul_max_upload")] public int? EmulMaxUploadSpeed { get; }
        /// <summary>
        /// Max FTP download speed in KB/s (“0” means unlimited).
        /// Currently <paramref name="FtpMaxDownloadSpeed"/> and <paramref name="HttpMaxDownloadSpeed"/>
        /// share the same config value. When both parameters are requested to be set at the same time, the requested
        /// <paramref name="FtpMaxDownloadSpeed"/> rate will overwrite the requested <paramref name="HttpMaxDownloadSpeed"/> rate.
        /// </summary>
        [JsonPropertyName("ftp_max_download")] public int? FtpMaxDownloadSpeed { get; }
        /// <summary>
        /// Max FTP download speed in KB/s (“0” means unlimited).
        /// Currently <paramref name="FtpMaxDownloadSpeed"/> and <paramref name="HttpMaxDownloadSpeed"/>
        /// share the same config value. When both parameters are requested to be set at the same time, the requested
        /// <paramref name="FtpMaxDownloadSpeed"/> rate will overwrite the requested <paramref name="HttpMaxDownloadSpeed"/> rate.
        /// </summary>
        [JsonPropertyName("http_max_download")] public int? HttpMaxDownloadSpeed { get; }
        /// <summary>
        /// Max NZB download speed in KB/s (“0” means unlimited)
        /// </summary>
        [JsonPropertyName("nzb_max_download")] public int? NzbMaxDownloadSpeed { get; }
        /// <summary>
        /// If Auto unzip service is enabled for users except admin or administrators grou
        /// </summary>
        [JsonPropertyName("unzip_service_enabled")] public bool? UnzipServiceEnable { get; }
        /// <summary>
        /// Default destination for fownloads
        /// </summary>
        [JsonPropertyName("default_destination")] public string? DefaultDestination { get; }
        /// <summary>
        /// Emule default destination. May be changed, if emul is already enable.
        /// </summary>
        [JsonPropertyName("emul_default_destination")] public string? EmulDefaultDestination { get; }
    }
}