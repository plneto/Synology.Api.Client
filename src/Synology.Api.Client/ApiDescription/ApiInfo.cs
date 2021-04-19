namespace Synology.Api.Client.ApiDescription
{
    public class ApiInfo : IApiInfo
    {
        public ApiInfo(string name, string path, int version, string sessionName = "")
        {
            Name = name;
            Path = path;
            Version = version;
            SessionName = sessionName;
        }

        public string Name { get; set; }

        public string Path { get; set; }

        public int Version { get; set; }

        public string SessionName { get; set; }
    }
}
