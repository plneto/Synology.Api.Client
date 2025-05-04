using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Synology.Api.Client.ApiDescription;
using Synology.Api.Client.Session;

namespace Synology.Api.Client;

public interface ISynologyHttpClient
{
    /// <summary>
    /// Sends a GET request to the Synology API.
    /// </summary>
    /// <param name="apiInfo">The API information.</param>
    /// <param name="apiMethod">The API method to call.</param>
    /// <param name="queryParams">The query parameters to include in the request.</param>
    /// <param name="session">The session to use for authentication. If null, the default session will be used.</param>
    /// <typeparam name="T">The type of the response.</typeparam>
    /// <returns>Returns a task that represents the asynchronous operation. The task result contains the response of type T.</returns>
    Task<T> GetAsync<T>(IApiInfo apiInfo, string apiMethod, Dictionary<string, string?> queryParams, ISynologySession? session = null);

    /// <summary>
    /// Sends a POST request to the Synology API.
    /// </summary>
    /// <param name="apiInfo">The API information.</param>
    /// <param name="apiMethod">The API method to call.</param>
    /// <param name="content">The content to include in the request body.</param>
    /// <param name="session">The session to use for authentication. If null, the default session will be used.</param>
    /// <typeparam name="T">The type of the response.</typeparam>
    /// <returns>Returns a task that represents the asynchronous operation. The task result contains the response of type T.</returns>
    Task<T> PostAsync<T>(IApiInfo apiInfo, string apiMethod, HttpContent? content, ISynologySession? session = null);
}