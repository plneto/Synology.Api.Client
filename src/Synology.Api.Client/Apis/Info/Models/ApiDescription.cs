namespace Synology.Api.Client.Apis.Info.Models
{
    public class ApiDescription
    {
        public string? Path { get; set; }

        public int MinVersion { get; set; }

        public int MaxVersion { get; set; }
    }
}
