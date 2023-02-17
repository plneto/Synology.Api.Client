using System;
using System.IO;
using System.IO.Abstractions;
using System.Net.Http;
using System.Net.Http.Headers;
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
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            if (string.IsNullOrWhiteSpace(destination))
            {
                throw new ArgumentNullException(nameof(destination));
            }

            var filename = _fileSystem.Path.GetFileName(filePath);
            var fileStream = _fileSystem.File.OpenRead(filePath);

            var fileContent = GetFileContent(fileStream, filename);

            return SendRequest(fileContent, destination, overwrite);
        }

        /// <inheritdoc />
        public Task<FileStationUploadResponse> UploadAsync(byte[] bytes, string filename, string destination, bool overwrite)
        {
            if (bytes is null)
            {
                throw new ArgumentNullException(nameof(bytes));
            }

            if (string.IsNullOrWhiteSpace(filename))
            {
                throw new ArgumentNullException(nameof(filename));
            }

            if (string.IsNullOrWhiteSpace(destination))
            {
                throw new ArgumentNullException(nameof(destination));
            }

            var memoryStream = new MemoryStream(bytes);
            var fileContent = GetFileContent(memoryStream, filename);

            return SendRequest(fileContent, destination, overwrite);
        }

        private Task<FileStationUploadResponse> SendRequest(StreamContent fileContent, string destination, bool overwrite)
        {
            var boundary = Guid.NewGuid().ToString();

            using (var formData = new MultipartFormDataContent(boundary))
            {
                // The request will fail if there are quotes around the boundary value
                formData.Headers.ContentType = new MediaTypeHeaderValue("multipart/form-data");
                formData.Headers.ContentType.Parameters.Add(new NameValueHeaderValue("boundary", boundary));

                var overwriteValue = overwrite ? "true" : "false";
                if (_apiInfo.Version >= 3)
                {
                    overwriteValue = overwrite ? "overwrite" : "skip";
                }

                formData.Add(GetStringContent("api", _apiInfo.Name));
                formData.Add(GetStringContent("version", _apiInfo.Version.ToString()));
                formData.Add(GetStringContent("method", "upload"));
                formData.Add(GetStringContent("path", destination));
                formData.Add(GetStringContent("overwrite", overwriteValue));
                //formData.Add(GetStringContent("create_parents", "true/false"));

                //prevent ObjectDisposedException
                //await fileContent.LoadIntoBufferAsync();
                formData.Add(fileContent);

                return _synologyHttpClient.PostAsync<FileStationUploadResponse>(_apiInfo, "upload", formData, _session);
            };
        }

        private StringContent GetStringContent(string name, string value)
        {
            var sc = new StringContent(value);
            sc.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
            {
                Name = $"\"{name}\""
            };

            //the API does not like the "Content-Type" header
            sc.Headers.ContentType = null;

            return sc;
        }

        private StreamContent GetFileContent(Stream stream, string filename)
        {
            var fileContent = new StreamContent(stream);
            fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
            {
                Name = "\"filename\"",//"\"file\"" seems also to work
                FileName = $"\"{filename}\""
            };

            return fileContent;
        }
    }
}
