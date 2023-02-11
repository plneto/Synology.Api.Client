using System.Collections.Generic;
using System.Threading.Tasks;
using Synology.Api.Client.ApiDescription;
using Synology.Api.Client.Apis.FileStation.Search.Models;
using Synology.Api.Client.Extensions;
using Synology.Api.Client.Session;

namespace Synology.Api.Client.Apis.FileStation.Search
{
    public class FileStationSearchEndpoint : IFileStationSearchEndpoint
    {
        private readonly ISynologyHttpClient _synologyHttpClient;
        private readonly IApiInfo _apiInfo;
        private readonly ISynologySession _session;

        public FileStationSearchEndpoint(ISynologyHttpClient synologyHttpClient, IApiInfo apiInfo,
            ISynologySession session)
        {
            _synologyHttpClient = synologyHttpClient;
            _apiInfo = apiInfo;
            _session = session;
        }

        /// <inheritdoc />
        public Task<FileStationSearchStartResponse> StartAsync(FileStationSearchStartRequest request)
        {
            var queryParams = new Dictionary<string, string>
            {
                { "folder_path", $"[\"{request.FolderPath}\"]" },
                { "recursive", request.Recursive.ToLowerString() },
                { "pattern", request.Pattern },
                { "extension", request.Extension },
            };

            return _synologyHttpClient.GetAsync<FileStationSearchStartResponse>(
                _apiInfo,
                "start",
                queryParams,
                _session);
        }

        /// <inheritdoc />
        public Task<FileStationSearchListResponse> ListAsync(string taskId, int offset = 0, int limit = -1)
        {
            var queryParams = new Dictionary<string, string>
            {
                { "taskid", taskId },
                { "offset", offset.ToString() },
                { "limit", limit.ToString() }
            };

            return _synologyHttpClient.GetAsync<FileStationSearchListResponse>(
                _apiInfo,
                "list",
                queryParams,
                _session);
        }
    }
}