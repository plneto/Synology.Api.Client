using System.Collections.Generic;
using System.Threading.Tasks;
using Synology.Api.Client.ApiDescription;
using Synology.Api.Client.Apis.FileStation.CopyMove.Models;
using Synology.Api.Client.Extensions;
using Synology.Api.Client.Session;
using Synology.Api.Client.Shared.Models;

namespace Synology.Api.Client.Apis.FileStation.CopyMove
{
    public class FileStationCopyMoveEndpoint : IFileStationCopyMoveEndpoint
    {
        private readonly ISynologyHttpClient _synologyHttpClient;
        private readonly IApiInfo _apiInfo;
        private readonly ISynologySession _session;

        public FileStationCopyMoveEndpoint(ISynologyHttpClient synologyHttpClient, IApiInfo apiInfo, ISynologySession session)
        {
            _synologyHttpClient = synologyHttpClient;
            _apiInfo = apiInfo;
            _session = session;
        }

        public Task<FileStationCopyMoveStartResponse> StartCopyAsync(string[] paths, string destination, bool overwrite)
        {
            var queryParams = GetCopyMoveQueryParams(paths, destination, overwrite, false);

            return _synologyHttpClient.GetAsync<FileStationCopyMoveStartResponse>(_apiInfo, "start", queryParams, _session);
        }

        public Task<FileStationCopyMoveStartResponse> StartMoveAsync(string[] paths, string destination, bool overwrite)
        {
            var queryParams = GetCopyMoveQueryParams(paths, destination, overwrite, true);

            return _synologyHttpClient.GetAsync<FileStationCopyMoveStartResponse>(_apiInfo, "start", queryParams, _session);
        }

        public Task<FileStationCopyMoveStatusResponse> GetStatusAsync(string taskId)
        {
            var queryParams = new Dictionary<string, string>
            {
                { "taskid",  taskId }
            };

            return _synologyHttpClient.GetAsync<FileStationCopyMoveStatusResponse>(
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

            return _synologyHttpClient.GetAsync<BaseApiResponse>(
                _apiInfo,
                "stop",
                queryParams,
                _session);
        }

        private Dictionary<string, string> GetCopyMoveQueryParams(string[] paths, string destination, bool overwrite, bool isMoveAction)
        {
            return new Dictionary<string, string>
            {
                { "path",  paths.ToCommaSeparatedAroundBrackets() },
                { "dest_folder_path", destination },
                { "overwrite", overwrite.ToLowerString() },
                { "remove_src", isMoveAction.ToLowerString() }
            };
        }
    }
}
