using System;
using System.Net;

namespace Synology.Api.Client.Exceptions
{
    public class UnexpectedResponseStatusException : Exception
    {
        public HttpStatusCode StatusCode { get; }

        public UnexpectedResponseStatusException(HttpStatusCode statusCode)
            : this($"Received unexpected response status \"{statusCode}\".", statusCode)
        {
        }

        public UnexpectedResponseStatusException(string message, HttpStatusCode statusCode, Exception innerException = null)
            : base(message, innerException)
        {
            StatusCode = statusCode;
        }
    }
}
