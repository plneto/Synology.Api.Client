using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Synology.Api.Client.ApiDescription;
using Synology.Api.Client.Apis.FileStation.CreateFolder.Models;
using Synology.Api.Client.Extensions;
using Synology.Api.Client.Session;

namespace Synology.Api.Client.Apis.FileStation.CreateFolder
{
    public class FileStationCreateFolderEndpoint : IFileStationCreateFolderEndpoint
    {
        private readonly ISynologyHttpClient _synologyHttpClient;
        private readonly IApiInfo _apiInfo;
        private readonly ISynologySession _session;

        public FileStationCreateFolderEndpoint(ISynologyHttpClient synologyHttpClient, IApiInfo apiInfo, ISynologySession session)
        {
            _synologyHttpClient = synologyHttpClient;
            _apiInfo = apiInfo;
            _session = session;
        }

        public Task<FileStationCreateFolderCreateResponse> CreateAsync(string[] paths, bool createParentFolders = false)
        {
            var queryParams = GetCreateQueryParams(paths, createParentFolders);

            return _synologyHttpClient.GetAsync<FileStationCreateFolderCreateResponse>(_apiInfo, "create", queryParams, _session);
        }

        private Dictionary<string, string> GetCreateQueryParams(string[] paths, bool createParentFolders)
        {
            var additionalParams = new[] { "real_path", "owner", "time" };
            var folderPaths = paths.Select(p => p.Substring(0, p.LastIndexOf('/')));
            var folderNames = paths.Select(p => p.Substring(p.LastIndexOf('/') + 1));

            return new Dictionary<string, string>
            {
                { "folder_path",  folderPaths.ToCommaSeparatedAroundBrackets() },
                { "name",  folderNames.ToCommaSeparatedAroundBrackets() },
                { "force_parent",  createParentFolders.ToLowerString() },
                { "additional",  additionalParams.ToCommaSeparatedAroundBrackets() }
            };
        }
    }
}
