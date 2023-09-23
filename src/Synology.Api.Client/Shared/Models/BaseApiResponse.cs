namespace Synology.Api.Client.Shared.Models
{
    public class BaseApiResponse
    {
        public BaseApiResponse()
        {
        }

        public BaseApiResponse(bool success)
        {
            Success = success;
        }

        public Error? Error { get; set; }

        public bool Success { get; set; }
    }
}
