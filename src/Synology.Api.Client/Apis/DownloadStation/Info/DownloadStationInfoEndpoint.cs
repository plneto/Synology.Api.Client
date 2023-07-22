using System.Collections.Generic;
using System.Threading.Tasks;
using Synology.Api.Client.ApiDescription;
using Synology.Api.Client.Apis.DownloadStation.Info.Models;
using Synology.Api.Client.Session;
using Synology.Api.Client.Shared.Models;

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
                new Dictionary<string, string?>(),
                _session);
        }

        public Task<DownloadStationServerConfig> GetConfigAsync()
        {
            return _synologyHttpClient.GetAsync<DownloadStationServerConfig>(
                _apiInfo,
                "getconfig",
                new Dictionary<string, string?>(),
                _session);
        }

        public Task<BaseApiResponse> SetServerConfigAsync(DownloadStationServerConfig config)
        {
            var queryParams = new Dictionary<string, string?>
            {
                { "bt_max_download", config.BtMaxDownloadSpeed?.ToString() },
                { "bt_max_upload", config.BtMaxUploadSpeed?.ToString() },
                { "emule_enabled", config.EmulEnable?.ToString() },
                { "emul_max_download", config.EmulMaxDownloadSpeed?.ToString() },
                { "emul_max_upload", config.EmulMaxUploadSpeed?.ToString() },
                { "ftp_max_download", config.FtpMaxDownloadSpeed?.ToString() },
                { "http_max_download", config.HttpMaxDownloadSpeed?.ToString() },
                { "nzb_max_download", config.NzbMaxDownloadSpeed?.ToString() },
                { "unzip_service_enabled", config.UnzipServiceEnable?.ToString() },
                { "default_destination", config.DefaultDestination },
                { "emul_default_destination", config.EmulDefaultDestination }
            };

            if (config.DefaultDestination != null || config.EmulDefaultDestination != null)
                _apiInfo.Version = 2;

            return _synologyHttpClient.GetAsync<BaseApiResponse>(
                _apiInfo,
                "setserverconfig",
                queryParams,
                _session);
        }
    }
}