using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using System.Net.Http.Json;
using System.Net;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using RichardSzalay.MockHttp;
using Synology.Api.Client.ApiDescription;
using Synology.Api.Client.Apis.Auth;
using Synology.Api.Client.Apis.FileStation.Upload;
using Synology.Api.Client.Apis.FileStation.Upload.Models;
using Synology.Api.Client.Errors;
using Synology.Api.Client.Exceptions;
using Synology.Api.Client.Session;
using Synology.Api.Client.Shared.Models;
using Synology.Api.Client.Tests.Fixtures;
using Xunit;

namespace Synology.Api.Client.Tests
{
    public class FileStationApiUploadEndpointTests : IClassFixture<SynologyFixture>, IDisposable
    {
        private readonly SynologyFixture _synologyFixture;
        private readonly Fixture _fixture;
        private readonly IApiInfo _apiInfo;
        private readonly MockHttpMessageHandler _mockHtttp;
        private readonly Uri _baseUriWithApiPath;
        private readonly IFileSystem _fileSystem;
        private readonly ISynologySession _session;
        private readonly string _destination;

        private const string FILEPATH_TO_UPLOAD = @"c:\myfile.txt";

        public FileStationApiUploadEndpointTests(SynologyFixture synologyFixture)
        {
            _synologyFixture = synologyFixture;
            _fixture = new Fixture();
            _apiInfo = _synologyFixture.ApisInfo.FileStationUploadApi;
            _mockHtttp = new MockHttpMessageHandler();
            _baseUriWithApiPath = new Uri(_synologyFixture.BaseUri, _apiInfo.Path);
            _session = new SynologySession(_fixture.Create<string>());
            _destination = _fixture.Create<string>();

            _fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
            {
                { FILEPATH_TO_UPLOAD, new MockFileData("Test file") }
            });
        }

        public void Dispose()
        {
            _mockHtttp.Dispose();
        }

        #region helper methods

        private IFileStationUploadEndpoint GetFileStationUploadEndpoint(IApiInfo apiInfoToUse = null)
        {
            var httpClient = _mockHtttp.ToHttpClient();
            httpClient.BaseAddress = _synologyFixture.BaseUri;

            var synologyHttpClient = new SynologyHttpClient(httpClient);

            var result = new FileStationUploadEndpoint(
                synologyHttpClient,
                apiInfoToUse ?? _apiInfo,
                _session,
                _fileSystem);

            return result;
        }

        #endregion

        #region FilePath

        [Fact]
        public async Task Upload_FilePath_ShouldCallCorrectUrl()
        {
            // Arrange
            var expectedResponse = new ApiResponse<FileStationUploadResponse>
            {
                Success = true,
                Data = _fixture.Create<FileStationUploadResponse>()
            };

            //expect certain request to server
            var request = _mockHtttp.Expect(System.Net.Http.HttpMethod.Post, _baseUriWithApiPath.ToString())
                 .WithQueryString(new Dictionary<string, string>
                 {
                    { "_sid" , _session.Sid }
                 });

            request.Respond(HttpStatusCode.OK, JsonContent.Create(expectedResponse));

            var fileStationUploadEndpoint = GetFileStationUploadEndpoint();

            // Act
            var result = await fileStationUploadEndpoint.UploadAsync(FILEPATH_TO_UPLOAD, _destination, overwrite: true);

            // Assert
            result.Should().BeEquivalentTo(expectedResponse.Data);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task Upload_FilePath_PathIsNullOrWhitespace_ShouldThrow(string filePath)
        {
            // Arrange
            var fileStationUploadEndpoint = GetFileStationUploadEndpoint();

            // Act
            var actDelegate = () => fileStationUploadEndpoint.UploadAsync(filePath, _destination, overwrite: true);

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(actDelegate);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task Upload_FilePath_DestinationIsNullOrWhitespace_ShouldThrow(string destination)
        {
            // Arrange
            var fileStationUploadEndpoint = GetFileStationUploadEndpoint();

            // Act
            var actDelegate = () => fileStationUploadEndpoint.UploadAsync(FILEPATH_TO_UPLOAD, destination, overwrite: true);

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(actDelegate);
        }

        #endregion

        #region Bytes

        [Fact]
        public async Task Upload_Bytes_ShouldCallCorrectUrl()
        {
            // Arrange
            var bytes = _fileSystem.File.ReadAllBytes(FILEPATH_TO_UPLOAD);
            var fileName = _fileSystem.Path.GetFileName(FILEPATH_TO_UPLOAD);

            var expectedResponse = new ApiResponse<FileStationUploadResponse>
            {
                Success = true,
                Data = _fixture.Create<FileStationUploadResponse>()
            };

            //expect certain request to server
            var request = _mockHtttp.Expect(System.Net.Http.HttpMethod.Post, _baseUriWithApiPath.ToString())
                 .WithQueryString(new Dictionary<string, string>
                 {
                    { "_sid" , _session.Sid }
                 });

            request.Respond(HttpStatusCode.OK, JsonContent.Create(expectedResponse));

            var fileStationUploadEndpoint = GetFileStationUploadEndpoint();

            // Act
            var result = await fileStationUploadEndpoint.UploadAsync(bytes, fileName, _destination, overwrite: true);

            // Assert
            result.Should().BeEquivalentTo(expectedResponse.Data);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task Upload_Bytes_FileNameIsNullOrWhitespace_ShouldThrow(string fileName)
        {
            // Arrange
            var bytes = _fileSystem.File.ReadAllBytes(FILEPATH_TO_UPLOAD);
            var fileStationUploadEndpoint = GetFileStationUploadEndpoint();

            // Act
            var actDelegate = () => fileStationUploadEndpoint.UploadAsync(bytes, fileName, _destination, overwrite: true);

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(actDelegate);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task Upload_Bytes_DestinationIsNullOrWhitespace_ShouldThrow(string destination)
        {
            // Arrange
            var bytes = _fileSystem.File.ReadAllBytes(FILEPATH_TO_UPLOAD);
            var fileName = _fileSystem.Path.GetFileName(FILEPATH_TO_UPLOAD);
            var fileStationUploadEndpoint = GetFileStationUploadEndpoint();

            // Act
            var actDelegate = () => fileStationUploadEndpoint.UploadAsync(bytes, fileName, destination, overwrite: true);

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(actDelegate);
        }

        [Fact]
        public async Task Upload_Bytes_BytesAreNull_ShouldThrow()
        {
            // Arrange
            var fileName = _fileSystem.Path.GetFileName(FILEPATH_TO_UPLOAD);
            var fileStationUploadEndpoint = GetFileStationUploadEndpoint();

            // Act
            var actDelegate = () => fileStationUploadEndpoint.UploadAsync(null, fileName, _destination, overwrite: true);

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(actDelegate);
        }

        #endregion

        [Fact]
        public async Task Upload_DestinationNotFound_ShouldDisplayCorrectError()
        {
            // Arrange
            var notFoundErrorCode = 408;
            var expectedErrorMessage = ErrorMessages.FileStationApiErrors.GetValueOrDefault(notFoundErrorCode);

            var expectedResponse = new ApiResponse<FileStationUploadResponse>
            {
                Success = false,
                Error = new Error
                {
                    Code = notFoundErrorCode
                }
            };

            //expect certain request to server
            var request = _mockHtttp.Expect(System.Net.Http.HttpMethod.Post, _baseUriWithApiPath.ToString())
                 .WithQueryString(new Dictionary<string, string>
                 {
                    { "_sid" , _session.Sid }
                 });

            request.Respond(HttpStatusCode.OK, JsonContent.Create(expectedResponse));

            var fileStationUploadEndpoint = GetFileStationUploadEndpoint();

            // Act
            var actDelegate = () => fileStationUploadEndpoint.UploadAsync(FILEPATH_TO_UPLOAD, _destination, overwrite: true);

            // Assert
            var apiException = await Assert.ThrowsAsync<SynologyApiException>(actDelegate);
            apiException.ErrorDescription.Should().BeEquivalentTo(expectedErrorMessage);
        }

        [Theory]
        [InlineData(2, true, "true")]
        [InlineData(2, false, "false")]
        [InlineData(3, true, "overwrite")]
        [InlineData(3, false, "skip")]
        public async Task Upload_DifferentApiVersion_ShouldSendCorrectValuesForOverwriteAndVersion(int apiVersion, bool overwrite, string overwriteValue)
        {
            // Arrange
            var apiInfo = new ApiInfo(_apiInfo.Name, _apiInfo.Path, apiVersion, _apiInfo.SessionName);

            var expectedRequestContentApiVersion = $"""
                Content-Disposition: form-data; name="version"

                {apiVersion}
                """;
            var expectedRequestContentOverwrite = $"""
                Content-Disposition: form-data; name="overwrite"

                {overwriteValue}
                """;

            var expectedResponse = new ApiResponse<FileStationUploadResponse>
            {
                Success = true,
                Data = _fixture.Create<FileStationUploadResponse>()
            };

            //expect certain request to server
            var request = _mockHtttp.Expect(System.Net.Http.HttpMethod.Post, _baseUriWithApiPath.ToString())
                .WithPartialContent(expectedRequestContentApiVersion)
                .WithPartialContent(expectedRequestContentOverwrite);

            request.Respond(HttpStatusCode.OK, JsonContent.Create(expectedResponse));

            var fileStationUploadEndpoint = GetFileStationUploadEndpoint(apiInfo);

            // Act
            var result = await fileStationUploadEndpoint.UploadAsync(FILEPATH_TO_UPLOAD, _destination, overwrite);

            // Assert
            result.Should().BeEquivalentTo(expectedResponse.Data);
        }
    }
}
