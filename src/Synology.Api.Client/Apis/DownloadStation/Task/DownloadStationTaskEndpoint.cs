using System.Threading.Tasks;
using Synology.Api.Client.ApiDescription;
using Synology.Api.Client.Apis.DownloadStation.Task.Models;
using Synology.Api.Client.Session;
using Synology.Api.Client.Shared.Models;

namespace Synology.Api.Client.Apis.DownloadStation.Task
{
    public class DownloadStationTaskEndpoint : IDownloadStationTaskEndpoint
    {
        private readonly ISynologyHttpClient _synologyHttpClient;
        private readonly IApiInfo _apiInfo;
        private readonly ISynologySession _session;

        public DownloadStationTaskEndpoint(ISynologyHttpClient synologyHttpClient, IApiInfo apiInfo, ISynologySession session)
        {
            _synologyHttpClient = synologyHttpClient;
            _apiInfo = apiInfo;
            _session = session;
        }
        
        /// <summary>
        /// No specific response. It returns an empty success response if completed without error.
        /// Remark: At the moment only the uri variant works (the other parameters are not used).
        /// This is due to errors in the official synology documentation. 
        /// </summary>
        /// <param name="request">Request parameters</param>
        /// <returns></returns>
        public Task<BaseApiResponse> CreateAsync(DownloadStationTaskCreateRequest request)
        {
            var queryParams = new
            {
                uri = request.Uri,
                file = request.File,
                username = request.Username,
                password = request.Password,
                unzip_password = request.UnzipPassword,
                destination = request.Destination
            };

            if (request.Destination != null)
                _apiInfo.Version = 2;

            if (request.Uri != null)
                _apiInfo.Version = 3;
            
            return _synologyHttpClient.GetAsync<BaseApiResponse>(_apiInfo, "create", queryParams, session: _session);
        }

        public Task<DownloadStationTaskListResponse> ListAsync()
        {
            var queryParams = new
            {
                additional = "detail,file"
            };

            return _synologyHttpClient.GetAsync<DownloadStationTaskListResponse>(_apiInfo, "list", queryParams, _session);
        }
    }
}
