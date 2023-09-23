using System.Collections.Generic;
using System.Threading.Tasks;
using Synology.Api.Client.ApiDescription;
using Synology.Api.Client.Apis.Info.Models;

namespace Synology.Api.Client.Apis.Info
{
    public class InfoEndpoint : IInfoEndpoint
    {
        private readonly ISynologyHttpClient _httpClient;
        private readonly IApiInfo _apiInfo;

        public InfoEndpoint(ISynologyHttpClient httpClient, IApiInfo apiInfo)
        {
            _httpClient = httpClient;
            _apiInfo = apiInfo;
        }

        public Task<InfoQueryResponse> QueryAsync()
        {
            return _httpClient.GetAsync<InfoQueryResponse>(
                _apiInfo, "query", 
                new Dictionary<string, string?> { { "query", "all" } });
        }
    }
}
