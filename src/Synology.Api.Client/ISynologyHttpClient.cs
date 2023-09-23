using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Synology.Api.Client.ApiDescription;
using Synology.Api.Client.Session;

namespace Synology.Api.Client
{
    public interface ISynologyHttpClient
    {
        Task<T?> GetAsync<T>(IApiInfo apiInfo, string apiMethod, Dictionary<string, string?> queryParams, ISynologySession? session = null);

        Task<T?> PostAsync<T>(IApiInfo apiInfo, string apiMethod, HttpContent content, ISynologySession? session = null);
    }
}
