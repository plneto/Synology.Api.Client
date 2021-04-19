using Synology.Api.Client.Apis.DownloadStation.Task;

namespace Synology.Api.Client.Apis.DownloadStation
{
    public interface IDownloadStationApi
    {
        IDownloadStationTaskEndpoint TaskEndpoint();
    }
}
