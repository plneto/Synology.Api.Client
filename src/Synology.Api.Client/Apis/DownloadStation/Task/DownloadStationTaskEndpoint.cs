using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Synology.Api.Client.ApiDescription;
using Synology.Api.Client.Apis.DownloadStation.Task.Models;
using Synology.Api.Client.Session;
using Synology.Api.Client.Shared.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Synology.Api.Client.Apis.DownloadStation.Task;

public class DownloadStationTaskEndpoint : IDownloadStationTaskEndpoint
{
    private readonly ISynologyHttpClient _synologyHttpClient;
    private readonly IApiInfo _apiInfo;
    private readonly ISynologySession _session;
    private readonly string[] _additional;

    public DownloadStationTaskEndpoint(ISynologyHttpClient synologyHttpClient, IApiInfo apiInfo, ISynologySession session)
    {
        _synologyHttpClient = synologyHttpClient;
        _apiInfo = apiInfo;
        _session = session;
        _additional = ["detail", "file", "transfer"];
    }

    /// <summary>
    /// No specific response. It returns an empty success response if completed without error.
    /// Remark: At the moment only the uri variant works (the other parameters are not used).
    /// This is due to errors in the official synology documentation. 
    /// </summary>
    /// <param name="request">Request parameters</param>
    /// <returns></returns>
    public Task<DownloadStationTaskCreateResponse> CreateAsync(DownloadStationTaskCreateRequest request)
    {
        var queryParams = new Dictionary<string, string?>
        {
            { "url", JsonSerializer.Serialize(request.Uri) },
            { "destination", JsonSerializer.Serialize(request.Destination) },
            { "type", JsonSerializer.Serialize("url") },
            { "create_list", JsonSerializer.Serialize(true) }
        };

        return _synologyHttpClient.GetAsync<DownloadStationTaskCreateResponse>(_apiInfo, "create", queryParams, session: _session);
    }

    public Task<DownloadStationTaskListResponse> ListAsync()
    {
        var queryParams = new Dictionary<string, string?>
        {
            { "additional",  JsonSerializer.Serialize(_additional) }
        };

    return _synologyHttpClient.GetAsync<DownloadStationTaskListResponse>(_apiInfo, "list", queryParams, _session);
}

    public async Task<DownloadStationTask> GetInfoAsync(string id)
    {
        var queryParams = new Dictionary<string, string?>
        {
            { "id",  JsonSerializer.Serialize(new []{id }) },
            {"additional", JsonSerializer.Serialize(_additional) }
        };

        var result = await _synologyHttpClient.GetAsync<DownloadStationTaskListResponse>(_apiInfo, "get", queryParams, _session);
        return result.Task.First();
    }

    public Task<DownloadStationTaskDeleteResponse> DeleteAsync(DownloadStationTaskDeleteRequest data)
    {
        var queryParam = new Dictionary<string, string?>
        {
            { "id", JsonSerializer.Serialize(data.Ids) },
            { "force_complete",JsonSerializer.Serialize(data.ForceComplete) }
        };

        return _synologyHttpClient.GetAsync<DownloadStationTaskDeleteResponse>(_apiInfo, "delete", queryParam, _session);
    }

    public Task<BaseApiResponse> PauseAsync(params string[] ids)
    {
        var queryParam = new Dictionary<string, string?>
        {
            { "id", JsonSerializer.Serialize(ids) }
        };

        return _synologyHttpClient.GetAsync<BaseApiResponse>(_apiInfo, "pause", queryParam, _session);
    }

    public Task<DownloadStationTaskResumeResponse> ResumeAsync(params string[] ids)
    {
        var queryParam = new Dictionary<string, string?>
        {
            { "id", JsonSerializer.Serialize(ids) }
        };

        return _synologyHttpClient.GetAsync<DownloadStationTaskResumeResponse>(_apiInfo, "resume", queryParam, _session);
    }
}
