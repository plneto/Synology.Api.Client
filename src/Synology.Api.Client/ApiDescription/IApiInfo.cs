namespace Synology.Api.Client.ApiDescription
{
    public interface IApiInfo
    {
        string Name { get; set; }

        string Path { get; set; }

        int Version { get; set; }

        string SessionName { get; set; }
    }
}
