using System.Threading.Tasks;
using Synology.Api.Client.ApiDescription;
using Synology.Api.Client.Apis.FileStation.List.Models;
using Synology.Api.Client.Extensions;
using Synology.Api.Client.Session;

namespace Synology.Api.Client.Apis.FileStation.List
{
    public class FileStationListEndpoint : IFileStationListEndpoint
    {
        private readonly ISynologyHttpClient _synologyHttpClient;
        private readonly IApiInfo _apiInfo;
        private readonly ISynologySession _session;

        public FileStationListEndpoint(ISynologyHttpClient synologyHttpClient, IApiInfo apiInfo, ISynologySession session)
        {
            _synologyHttpClient = synologyHttpClient;
            _apiInfo = apiInfo;
            _session = session;
        }

        public Task<FileStationListShareResponse> ListSharesAsync()
        {
            var additionalParams = new[] { "real_path", "owner", "time" };

            var queryParams = new
            {
                additional = additionalParams.ToCommaSeparatedAroundBrackets()
            };

            return _synologyHttpClient.GetAsync<FileStationListShareResponse>(
                _apiInfo,
                "list_share",
                queryParams,
                _session);
        }
    }
}
