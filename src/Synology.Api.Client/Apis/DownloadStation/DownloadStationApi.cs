using Synology.Api.Client.ApiDescription;
using Synology.Api.Client.Apis.DownloadStation.Task;
using Synology.Api.Client.Session;

namespace Synology.Api.Client.Apis.DownloadStation
{
    public class DownloadStationApi : IDownloadStationApi
    {
        private readonly ISynologyHttpClient _synologyHttpClient;
        private readonly IApisInfo _apisInfo;
        private readonly ISynologySession _session;

        public DownloadStationApi(ISynologyHttpClient synologyHttpClient, IApisInfo apisInfo, ISynologySession session)
        {
            _synologyHttpClient = synologyHttpClient;
            _apisInfo = apisInfo;
            _session = session;
        }

        public IDownloadStationTaskEndpoint TaskEndpoint()
        {
            return new DownloadStationTaskEndpoint(_synologyHttpClient, _apisInfo.DownloadStationTaskApi, _session);
        }
    }
}
