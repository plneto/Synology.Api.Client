using System;
using Synology.Api.Client.ApiDescription;

namespace Synology.Api.Client.Exceptions
{
    public class SynologyApiException : Exception
    {
        public string ApiMethod { get; }

        public IApiInfo ApiInfo { get; }

        public int ErrorCode { get; }

        public SynologyApiException(IApiInfo apiInfo, string apiMethod, int errorCode)
            : this($"The Synology API Request failed with code \"{errorCode}\".\n" +
                  $"API: \"{apiInfo.Name}\" \n" +
                  $"Method: \"{apiMethod}\" \n" +
                  $"Version: \"{apiInfo.Version}\" ", apiInfo, apiMethod, errorCode)
        {
        }

        public SynologyApiException(
            string message,
            IApiInfo apiInfo,
            string apiMethod,
            int errorCode,
            Exception innerException = null)
            : base(message, innerException)
        {
            ApiInfo = apiInfo;
            ApiMethod = apiMethod;
            ErrorCode = errorCode;
        }
    }
}
