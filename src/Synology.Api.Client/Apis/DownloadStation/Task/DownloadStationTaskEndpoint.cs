using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Synology.Api.Client.ApiDescription;
using Synology.Api.Client.Apis.DownloadStation.Task.Models;
using Synology.Api.Client.Extensions;
using Synology.Api.Client.Session;
using Synology.Api.Client.Shared.Models;

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
            { "url", request.Uri.ToCommaSeparatedAroundBrackets() },
            { "type", "url".ToQuotedString() },
            { "create_list", true.ToLowerString() }
        };

        if (request.Destination != null)
        {
            queryParams.Add("destination", request.Destination.ToQuotedString());
        }

        return _synologyHttpClient.GetAsync<DownloadStationTaskCreateResponse>(_apiInfo, "create", queryParams, session: _session);
    }

    public Task<DownloadStationTaskListResponse> ListAsync()
    {
        var queryParams = new Dictionary<string, string?>
        {
            { "additional",  _additional.ToCommaSeparatedAroundBrackets() }
        };

        return _synologyHttpClient.GetAsync<DownloadStationTaskListResponse>(_apiInfo, "list", queryParams, _session);
    }

    public async Task<DownloadStationTask> GetInfoAsync(string id)
    {
        var queryParams = new Dictionary<string, string?>
        {
            { "id",  new[] { id }.ToCommaSeparatedAroundBrackets() },
            { "additional", _additional.ToCommaSeparatedAroundBrackets() }
        };

        var result = await _synologyHttpClient.GetAsync<DownloadStationTaskGetResponse>(_apiInfo, "get", queryParams, _session);
        return result.Tasks!.First();
    }

    public Task<DownloadStationTaskDeleteResponse> DeleteAsync(DownloadStationTaskDeleteRequest data)
    {
        var queryParam = new Dictionary<string, string?>
        {
            { "id", data.Ids.ToCommaSeparatedAroundBrackets() },
            { "force_complete", data.ForceComplete.ToLowerString() }
        };

        return _synologyHttpClient.GetAsync<DownloadStationTaskDeleteResponse>(_apiInfo, "delete", queryParam, _session);
    }

    public Task<BaseApiResponse> PauseAsync(params string[] ids)
    {
        var queryParam = new Dictionary<string, string?>
        {
            { "id", ids.ToCommaSeparatedAroundBrackets() }
        };

        return _synologyHttpClient.GetAsync<BaseApiResponse>(_apiInfo, "pause", queryParam, _session);
    }

    public Task<DownloadStationTaskResumeResponse> ResumeAsync(params string[] ids)
    {
        var queryParam = new Dictionary<string, string?>
        {
            { "id", ids.ToCommaSeparatedAroundBrackets() }
        };

        return _synologyHttpClient.GetAsync<DownloadStationTaskResumeResponse>(_apiInfo, "resume", queryParam, _session);
    }
}
