using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Web;
using Synology.Api.Client.ApiDescription;
using Synology.Api.Client.Constants;
using Synology.Api.Client.Errors;
using Synology.Api.Client.Exceptions;
using Synology.Api.Client.Session;
using Synology.Api.Client.Shared.Models;

namespace Synology.Api.Client;

public class SynologyHttpClient : ISynologyHttpClient
{
    private readonly HttpClient _httpClient;

    public SynologyHttpClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<T> GetAsync<T>(IApiInfo apiInfo, string apiMethod, Dictionary<string, string?> queryParams, ISynologySession? session = null)
    {
        var uri = GetBaseUri(_httpClient.BaseAddress!, apiInfo.Path);
        var uriBuilder = new UriBuilder(uri);

        uriBuilder.Query = BuildQueryString(uriBuilder, apiInfo, apiMethod, queryParams, session);

        using var response = await _httpClient.GetAsync(uriBuilder.Uri);
        return await HandleSynologyResponse<T>(response, apiInfo, apiMethod);
    }

    public async Task<T> PostAsync<T>(IApiInfo apiInfo, string apiMethod, HttpContent content, ISynologySession? session = null)
    {
        var uri = GetBaseUri(_httpClient.BaseAddress!, apiInfo.Path);
        var uriBuilder = new UriBuilder(uri);

        if (session != null)
        {
            uriBuilder.Query = $"_sid={session.Sid}";
        }

        //just for debugging to check values more easily
        //var headersAsString = content.Headers.ToString();
        //var contentAsString = await content.ReadAsStringAsync();

        using var response = await _httpClient.PostAsync(uriBuilder.Uri, content);
        return await HandleSynologyResponse<T>(response, apiInfo, apiMethod);
    }

    private static Uri GetBaseUri(Uri baseAddress, string apiPath)
    {
        var baseUri = baseAddress.ToString().TrimEnd('/');
        apiPath = apiPath.TrimStart('/');

        return new Uri(baseUri + "/" + apiPath);
    }

    private static string BuildQueryString(UriBuilder uriBuilder, IApiInfo apiInfo, string apiMethod, Dictionary<string, string?> queryParams, ISynologySession? session = null)
    {
        var query = HttpUtility.ParseQueryString(uriBuilder.Query);
        query["api"] = apiInfo.Name;
        query["version"] = apiInfo.Version.ToString();
        query["method"] = apiMethod;

        foreach (var curPair in queryParams)
        {
            query[curPair.Key] = curPair.Value;
        }

        if (!string.IsNullOrWhiteSpace(apiInfo.SessionName))
        {
            query["session"] = apiInfo.SessionName;
        }

        if (session != null)
        {
            query["_sid"] = session.Sid;
        }

        return query.ToString()!;
    }

    private async Task<T> HandleSynologyResponse<T>(HttpResponseMessage httpResponse, IApiInfo apiInfo, string apiMethod)
    {
        switch (httpResponse.StatusCode)
        {
            case HttpStatusCode.OK:
                {
                    var response = await httpResponse.Content.ReadFromJsonAsync<ApiResponse<T>>();

                    HandleErrors(apiInfo, apiMethod, response);

                    if (typeof(T) == typeof(BaseApiResponse))
                    {
                        return (T)Activator.CreateInstance(typeof(T), [response!.Success])!;
                    }

                    return response!.Data;
                }
            default:
                throw new UnexpectedResponseStatusException(httpResponse.StatusCode);
        }
    }

    private void HandleErrors<T>(IApiInfo apiInfo, string apiMethod, ApiResponse<T>? response)
    {
        if (response is { Success: true })
        {
            return;
        }

        var errorDescription = GetErrorMessage(response?.Error?.Code ?? 0, apiInfo.Name);

        var synologyApiException = new SynologyApiException(apiInfo, apiMethod, response?.Error?.Code ?? 0, errorDescription);

        //add additional error details if present
        if (response?.Error?.Errors == null || !response.Error.Errors.Any())
        {
            throw synologyApiException;
        }

        foreach (var curError in response.Error.Errors)
        {
            var errorMessage = GetErrorMessage(curError.Code, apiInfo.Name);
            synologyApiException.Data.Add($"[{curError.Code}] {errorMessage}", curError.Path);
        }

        throw synologyApiException;
    }

    private string GetErrorMessage(int errorCode, string apiName)
    {
        var errorDescription = "";

        if (ErrorMessages.CommonErrors.ContainsKey(errorCode))
        {
            ErrorMessages.CommonErrors.TryGetValue(errorCode, out errorDescription);
        }
        else if (apiName == ApiNames.AuthApiName)
        {
            ErrorMessages.AuthApiErrors.TryGetValue(errorCode, out errorDescription);
        }
        else if (apiName.Contains("FileStation"))
        {
            ErrorMessages.FileStationApiErrors.TryGetValue(errorCode, out errorDescription);
        }
        else if (apiName == ApiNames.DownloadStationTaskApiName)
        {
            ErrorMessages.DownloadStationTaskApiErrors.TryGetValue(errorCode, out errorDescription);
        }
        else if (apiName == ApiNames.DownloadStationBtSearchApiName)
        {
            ErrorMessages.DownloadStationBtSearchApiErrors.TryGetValue(errorCode, out errorDescription);
        }

        return errorDescription ?? string.Empty;
    }
}