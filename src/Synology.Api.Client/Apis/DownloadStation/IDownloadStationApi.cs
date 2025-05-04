using Synology.Api.Client.Apis.DownloadStation.Info;

namespace Synology.Api.Client.Apis.DownloadStation;

public interface IDownloadStationApi
{
    IDownloadStationTaskEndpoint TaskEndpoint();
    IDownloadStationInfoEndpoint InfoEndpoint();
}