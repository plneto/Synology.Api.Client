using Synology.Api.Client.Apis.DownloadStation.Info;
using Synology.Api.Client.Apis.DownloadStation.Task;

namespace Synology.Api.Client.Apis.DownloadStation
{
    public interface IDownloadStationApi
    {
        IDownloadStationTaskEndpoint TaskEndpoint();
        IDownloadStationInfoEndpoint InfoEndpoint();
    }
}
