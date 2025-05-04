using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Synology.Api.Client.ApiDescription;
using Synology.Api.Client.Apis.FileStation.Search.Models;
using Synology.Api.Client.Extensions;
using Synology.Api.Client.Session;

namespace Synology.Api.Client.Apis.FileStation.Search;

public class FileStationSearchEndpoint : IFileStationSearchEndpoint
{
    private readonly ISynologyHttpClient _synologyHttpClient;
    private readonly IApiInfo _apiInfo;
    private readonly ISynologySession _session;

    public FileStationSearchEndpoint(ISynologyHttpClient synologyHttpClient, IApiInfo apiInfo, ISynologySession session)
    {
        _synologyHttpClient = synologyHttpClient;
        _apiInfo = apiInfo;
        _session = session;
    }

    /// <inheritdoc />
    public Task<FileStationSearchStartResponse> StartAsync(FileStationSearchStartRequest request)
    {
        if (request.FolderPath == null)
        {
            throw new ArgumentNullException(nameof(request.FolderPath));
        }
        
        var formData = new Dictionary<string, string?>
        {
            { "folder_path", request.FolderPath.ToJsonArray() },
            { "recursive", request.Recursive.ToLowerString() },
            
            // these parameters are not documented in the PDF, but they are used in the web UI
            { "search_content", request.SearchContent.ToLowerString() },
            { "search_type", "simple".ToQuotedString() },
            { "api", _apiInfo.Name },
            { "method", "start" },
            { "version", _apiInfo.Version.ToString() }
        };
        
        if (!string.IsNullOrWhiteSpace(request.Pattern))
        {
            formData.Add("pattern", request.Pattern.ToQuotedString());
        }
        
        if (!string.IsNullOrWhiteSpace(request.Extension))
        {
            formData.Add("extension", request.Extension.ToQuotedString());
        }
        
        // the PDF documentation uses a GET but the web UI uses a POST
        return _synologyHttpClient.PostAsync<FileStationSearchStartResponse>(
            _apiInfo,
            "start",
            new FormUrlEncodedContent(formData),
            _session);
    }

    /// <inheritdoc />
    public Task<FileStationSearchListResponse> ListAsync(string taskId, int offset = 0, int limit = -1)
    {
        var additionalParams = new[] { "real_path", "owner", "time", "size" };
        
        var formData = new Dictionary<string, string?>
        {
            { "taskid", taskId.ToQuotedString() },
            { "offset", offset.ToString() },
            { "limit", limit.ToString() },
            { "additional",  additionalParams.ToJsonArray() },
            { "filetype", "all".ToQuotedString() },
            { "api", _apiInfo.Name },
            { "method", "list" },
            { "version", _apiInfo.Version.ToString() }
        };

        // the PDF documentation uses a GET but the web UI uses a POST
        return _synologyHttpClient.PostAsync<FileStationSearchListResponse>(
            _apiInfo,
            "list",
            new FormUrlEncodedContent(formData),
            _session);
    }
}