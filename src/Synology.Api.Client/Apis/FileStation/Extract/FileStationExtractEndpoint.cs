using System.Collections.Generic;
using System.Threading.Tasks;
using Synology.Api.Client.ApiDescription;
using Synology.Api.Client.Apis.FileStation.Extract.Models;
using Synology.Api.Client.Extensions;
using Synology.Api.Client.Session;
using Synology.Api.Client.Shared.Models;

namespace Synology.Api.Client.Apis.FileStation.Extract
{
    public class FileStationExtractEndpoint : IFileStationExtractEndpoint
    {
        private readonly ISynologyHttpClient _synologyHttpClient;
        private readonly IApiInfo _apiInfo;
        private readonly ISynologySession _session;

        public FileStationExtractEndpoint(ISynologyHttpClient synologyHttpClient, IApiInfo apiInfo, ISynologySession session)
        {
            _synologyHttpClient = synologyHttpClient;
            _apiInfo = apiInfo;
            _session = session;
        }

        public Task<FileStationExtractStartResponse> StartAsync(string filePath, string destination, bool overwrite)
        {
            var queryParams = new Dictionary<string, string>
            {
                { "file_path",  filePath },
                { "dest_folder_path",  destination },
                { "overwrite",  overwrite.ToLowerString() }
            };

            return _synologyHttpClient.GetAsync<FileStationExtractStartResponse>(
                _apiInfo,
                "start",
                queryParams,
                _session);
        }

        public Task<FileStationExtractStatusResponse> GetStatusAsync(string taskId)
        {
            var queryParams = new Dictionary<string, string>
            {
                { "taskid",  taskId }
            };

            return _synologyHttpClient.GetAsync<FileStationExtractStatusResponse>(
                _apiInfo,
                "status",
                queryParams,
                _session);
        }

        public Task<BaseApiResponse> StopAsync(string taskId)
        {
            var queryParams = new Dictionary<string, string>
            {
                { "taskid",  taskId }
            };

            return _synologyHttpClient.GetAsync<BaseApiResponse>(_apiInfo, "stop", queryParams, _session);
        }

        public Task<FileStationExtractListResponse> ListFilesAsync(string filePath)
        {
            var queryParams = new Dictionary<string, string>
            {
                { "file_path",  filePath }
            };

            return _synologyHttpClient.GetAsync<FileStationExtractListResponse>(
                _apiInfo,
                "list",
                queryParams,
                _session);
        }
    }
}
