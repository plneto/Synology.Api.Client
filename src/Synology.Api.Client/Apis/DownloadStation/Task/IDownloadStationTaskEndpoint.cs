using Synology.Api.Client.Apis.DownloadStation.Task.Models;
using Synology.Api.Client.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Synology.Api.Client.Apis.DownloadStation.Task;

public interface IDownloadStationTaskEndpoint
{
    Task<DownloadStationTaskListResponse> ListAsync();

    Task<DownloadStationTask> GetInfoAsync(string id);

    Task<BaseApiResponse> CreateAsync(DownloadStationTaskCreateRequest request);

    Task<IEnumerable<DownloadStationTaskDeleteResponse>> DeleteAsync(DownloadStationTaskDeleteRequest data);

    Task<IEnumerable<DownloadStationPauseResponse>> PauseAsync(params string[] data);

    Task<IEnumerable<DownloadStationTaskResumeResponse>> ResumeAsync(params string[] ids);
}