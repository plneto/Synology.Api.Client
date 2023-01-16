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

        public Task<DownloadStationGetConfigResponse> GetConfigAsync()
        {
            return _synologyHttpClient.GetAsync<DownloadStationGetConfigResponse>(
                _apiInfo,
                "getconfig",
                session: _session);
        }
    }
}