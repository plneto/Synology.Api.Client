using System;
using System.IO;
using System.IO.Abstractions;
using System.Net.Http;
using System.Threading.Tasks;
using Synology.Api.Client.ApiDescription;
using Synology.Api.Client.Apis.FileStation.Upload.Models;
using Synology.Api.Client.Session;

namespace Synology.Api.Client.Apis.FileStation.Upload
{
    public class FileStationUploadEndpoint : IFileStationUploadEndpoint
    {
        private readonly ISynologyHttpClient _synologyHttpClient;
        private readonly IApiInfo _apiInfo;
        private readonly ISynologySession _session;
        private readonly IFileSystem _fileSystem;

        public FileStationUploadEndpoint(ISynologyHttpClient synologyHttpClient, IApiInfo apiInfo, ISynologySession session)
            : this(synologyHttpClient, apiInfo, session, new FileSystem())
        {
        }

        public FileStationUploadEndpoint(ISynologyHttpClient synologyHttpClient,
                                         IApiInfo apiInfo,
                                         ISynologySession session,
                                         IFileSystem fileSystem)
        {
            _synologyHttpClient = synologyHttpClient;
            _apiInfo = apiInfo;
            _session = session;
            _fileSystem = fileSystem;
        }

        /// <inheritdoc />
        public Task<FileStationUploadResponse> UploadAsync(string filePath, string destination, bool overwrite)
        {
            var filename = _fileSystem.Path.GetFileName(filePath);
            var fileStream = _fileSystem.File.OpenRead(filePath);

            return SendRequest(GetFileContent(fileStream, filename), destination, overwrite);
        }

        /// <inheritdoc />
        public Task<FileStationUploadResponse> UploadAsync(byte[] bytes, string filename, string destination, bool overwrite)
        {
            var memoryStream = new MemoryStream(bytes);

            return SendRequest(GetFileContent(memoryStream, filename), destination, overwrite);
        }

        private Task<FileStationUploadResponse> SendRequest(StreamContent fileContent, string destination, bool overwrite)
        {
            string boundaryDelimiter = $"----------{DateTime.Now.Ticks:x}";

            using (var formData = new MultipartFormDataContent(boundaryDelimiter))
            {
                var overwriteValue = overwrite ? "overwrite" : "skip";

                formData.Add(GetStringContent("api", _apiInfo.Name));
                formData.Add(GetStringContent("version", _apiInfo.Version.ToString()));
                formData.Add(GetStringContent("method", "upload"));
                formData.Add(GetStringContent("path", destination));
                formData.Add(GetStringContent("overwrite", overwriteValue));
                formData.Add(fileContent);

                // Remove quotes from ContentType Header
                // The request will fail if there are quotes around the boundary value 
                formData.Headers.Remove("Content-Type");
                formData.Headers.TryAddWithoutValidation("Content-Type", $"multipart/form-data; boundary={boundaryDelimiter}");

                return _synologyHttpClient.PostAsync<FileStationUploadResponse>(_apiInfo, "upload", formData, _session);
            };
        }

        private StringContent GetStringContent(string name, string value)
        {
            var sc = new StringContent(value);
            sc.Headers.Add("Content-Disposition", $"form-data; name=\"{name}\"");
            sc.Headers.ContentType = null;
            return sc;
        }

        private StreamContent GetFileContent(Stream stream, string filename)
        {
            var fileContent = new StreamContent(stream);
            fileContent.Headers.Add("Content-Disposition", $"form-data; name=\"filename\"; filename=\"{filename}\"");

            return fileContent;
        }
    }
}
