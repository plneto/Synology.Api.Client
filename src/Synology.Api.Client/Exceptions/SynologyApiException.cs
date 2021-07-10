using System;
using Synology.Api.Client.ApiDescription;

namespace Synology.Api.Client.Exceptions
{
    public class SynologyApiException : Exception
    {
        public string ApiMethod { get; }

        public IApiInfo ApiInfo { get; }

        public int ErrorCode { get; }

        public string ErrorDescription { get; }

        public SynologyApiException(IApiInfo apiInfo, string apiMethod, int errorCode, string errorDescription = "")
            : this($"The Synology API Request failed.\n" +
                  $"Error Code: \"{errorCode}\"\n" +
                  $"Error Description: \"{errorDescription}\"\n" +
                  $"API: \"{apiInfo.Name}\" \n" +
                  $"Method: \"{apiMethod}\" \n" +
                  $"Version: \"{apiInfo.Version}\"", apiInfo, apiMethod, errorCode, errorDescription)
        {
        }

        public SynologyApiException(
            string message,
            IApiInfo apiInfo,
            string apiMethod,
            int errorCode,
            string errorDescription,
            Exception innerException = null)
            : base(message, innerException)
        {
            ApiInfo = apiInfo;
            ApiMethod = apiMethod;
            ErrorCode = errorCode;
            ErrorDescription = errorDescription;
        }
    }
}
