using System.Threading.Tasks;
using Synology.Api.Client.Apis.DownloadStation.Task.Models;

namespace Synology.Api.Client.Apis.DownloadStation.Task
{
    public interface IDownloadStationTaskEndpoint
    {
        Task<DownloadStationTaskListResponse> ListAsync();

        Task<DownloadStationTaskDeleteResponse> DeleteAsync(DownloadStationTaskDeleteRequest data);
    }
}
