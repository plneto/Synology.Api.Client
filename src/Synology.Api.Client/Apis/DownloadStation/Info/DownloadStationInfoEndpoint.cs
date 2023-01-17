using System.Threading.Tasks;
using Synology.Api.Client.ApiDescription;
using Synology.Api.Client.Apis.DownloadStation.Info.Models;
using Synology.Api.Client.Session;

namespace Synology.Api.Client.Apis.DownloadStation.Info
{
    public class DownloadStationInfoEndpoint : IDownloadStationInfoEndpoint
    {
        private readonly ISynologyHttpClient _synologyHttpClient;
        private readonly IApiInfo _apiInfo;
        private readonly ISynologySession _session;

        public DownloadStationInfoEndpoint(ISynologyHttpClient synologyHttpClient, IApiInfo apiInfo, ISynologySession session)
        {
            _synologyHttpClient = synologyHttpClient;
            _apiInfo = apiInfo;
            _session = session;
        }

        public Task<DownloadStationInfoResponse> InfoAsync()
        {
            return _synologyHttpClient.GetAsync<DownloadStationInfoResponse>(
                _apiInfo,
                "getinfo",
                session: _session);
        }

        public Task<DownloadStationServerConfig> GetConfigAsync()
        {
            return _synologyHttpClient.GetAsync<DownloadStationServerConfig>(
                _apiInfo,
                "getconfig",
                session: _session);
        }

        public Task<DownloadStationServerConfig> SetServerConfigAsync(DownloadStationServerConfig config)
        {

            var queryParams = new
            {
                bt_max_download = config.BtMaxDownloadSpeed,
                bt_max_upload = config.BtMaxUploadSpeed,
                emule_enabled = config.EmulEnable,
                emul_max_download = config.EmulMaxDownloadSpeed,
                emul_max_upload = config.EmulMaxUploadSpeed,
                ftp_max_download = config.FtpMaxDownloadSpeed,
                http_max_download = config.HttpMaxDownloadSpeed,
                nzb_max_download = config.NzbMaxDownloadSpeed,
                unzip_service_enabled = config.UnzipServiceEnable,
                default_destination = config.DefaultDestination,
                emul_default_destination = config.EmulDefaultDestination
            };

            if (!(config.DefaultDestination is null || config.EmulDefaultDestination is null))
                _apiInfo.Version = 2;

            return _synologyHttpClient.GetAsync<DownloadStationServerConfig>(
                _apiInfo,
                "setserverconfig",
                queryParams,
                _session);
        }
    }
}