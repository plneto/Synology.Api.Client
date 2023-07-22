using System.Collections.Generic;
using System.Threading.Tasks;
using Synology.Api.Client.Apis.DownloadStation.Task.Models;
using Synology.Api.Client.Shared.Models;

namespace Synology.Api.Client.Apis.DownloadStation.Task
{
    public interface IDownloadStationTaskEndpoint
    {
        Task<DownloadStationTaskListResponse> ListAsync();
        
        Task<BaseApiResponse> CreateAsync(DownloadStationTaskCreateRequest request);

        Task<IEnumerable<DownloadStationTaskDeleteResponse>> DeleteAsync(DownloadStationTaskDeleteRequest data);

        Task<IEnumerable<DownloadStationPauseResponse>> PauseAsync(params string[] data);

        Task<IEnumerable<DownloadStationTaskResumeResponse>> ResumeAsync(params string[] ids);
    }
}
