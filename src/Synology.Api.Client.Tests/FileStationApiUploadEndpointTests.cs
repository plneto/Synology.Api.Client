using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using AutoFixture;
using FluentAssertions;
using RichardSzalay.MockHttp;
using Synology.Api.Client.ApiDescription;
using Synology.Api.Client.Apis.FileStation.Upload;
using Synology.Api.Client.Apis.FileStation.Upload.Models;
using Synology.Api.Client.Errors;
using Synology.Api.Client.Exceptions;
using Synology.Api.Client.Session;
using Synology.Api.Client.Shared.Models;
using Synology.Api.Client.Tests.Fixtures;
using Xunit;

namespace Synology.Api.Client.Tests;

public class FileStationApiUploadEndpointTests : IClassFixture<SynologyFixture>, IDisposable
{
    private readonly SynologyFixture _synologyFixture;
    private readonly Fixture _fixture;
    private readonly IApiInfo _apiInfo;
    private readonly MockHttpMessageHandler _mockHttp;
    private readonly Uri _baseUriWithApiPath;
    private readonly IFileSystem _fileSystem;
    private readonly ISynologySession _session;
    private readonly string _destination;

    private const string FilepathToUpload = @"c:\myfile.txt";
    private const string NonAsciiFileName = "文件.txt";

    public FileStationApiUploadEndpointTests(SynologyFixture synologyFixture)
    {
        _synologyFixture = synologyFixture;
        _fixture = new Fixture();
        _apiInfo = _synologyFixture.ApisInfo.FileStationUploadApi;
        _mockHttp = new MockHttpMessageHandler();
        _baseUriWithApiPath = _synologyFixture.GetBaseUriWithPath(_apiInfo.Path);
        _session = new SynologySession(_fixture.Create<string>());
        _destination = _fixture.Create<string>();

        _fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
        {
            {FilepathToUpload, new MockFileData("Test file")},
            {NonAsciiFileName, new MockFileData("Test file")},
        });
    }

    public void Dispose()
    {
        _mockHttp.Dispose();
    }

    #region helper methods

    private IFileStationUploadEndpoint GetFileStationUploadEndpoint(IApiInfo? apiInfoToUse = null)
    {
        var httpClient = _mockHttp.ToHttpClient();
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
        var request = _mockHttp.Expect(HttpMethod.Post, _baseUriWithApiPath.ToString())
            .WithQueryString(new Dictionary<string, string>
            {
                {"_sid", _session.Sid}
            });

        request.Respond(HttpStatusCode.OK, JsonContent.Create(expectedResponse));

        var fileStationUploadEndpoint = GetFileStationUploadEndpoint();

        // Act
        var result = await fileStationUploadEndpoint.UploadAsync(FilepathToUpload, _destination, overwrite: true);

        // Assert
        result.Should().BeEquivalentTo(expectedResponse.Data);
    }

    [Fact]
    public async Task Upload_WhenTheFilenameHasNonAsciiCharacters_ThenTheRequestIsFormattedCorrectly()
    {
        // Arrange
        var expectedResponse = new ApiResponse<FileStationUploadResponse>
        {
            Success = true,
            Data = _fixture.Create<FileStationUploadResponse>()
        };

        var expectedFilename = Encoding.UTF8.GetBytes(NonAsciiFileName)
            .Aggregate("", (current, b) => current + (char)b);

        var urlEncodedFilename = $"UTF-8''{Uri.EscapeDataString(NonAsciiFileName)}";

        //expect certain request to server
        var request = _mockHttp.Expect(HttpMethod.Post, _baseUriWithApiPath.ToString())
            .WithQueryString(new Dictionary<string, string>
            {
                {"_sid", _session.Sid}
            })
            .With(x =>
            {
                var content = x.Content as MultipartFormDataContent;
                var header = content?
                    .Where(y =>
                    {
                        var contentDisposition = y.Headers.ContentDisposition?.ToString();

                        return contentDisposition != null
                               && contentDisposition.Contains(expectedFilename)
                               && contentDisposition.Contains(urlEncodedFilename);
                    });

                return header?.Any() ?? false;
            });

        request.Respond(HttpStatusCode.OK, JsonContent.Create(expectedResponse));

        var fileStationUploadEndpoint = GetFileStationUploadEndpoint();

        // Act
        var result = await fileStationUploadEndpoint.UploadAsync(NonAsciiFileName, _destination, overwrite: true);

        // Assert
        result.Should().BeEquivalentTo(expectedResponse.Data);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public async Task Upload_FilePath_PathIsNullOrWhitespace_ShouldThrow(string? filePath)
    {
        // Arrange
        var fileStationUploadEndpoint = GetFileStationUploadEndpoint();

        // Act
        var actDelegate = () => fileStationUploadEndpoint.UploadAsync(filePath!, _destination, overwrite: true);

        // Assert
        await Assert.ThrowsAsync<ArgumentNullException>(actDelegate);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public async Task Upload_FilePath_DestinationIsNullOrWhitespace_ShouldThrow(string? destination)
    {
        // Arrange
        var fileStationUploadEndpoint = GetFileStationUploadEndpoint();

        // Act
        var actDelegate = () => fileStationUploadEndpoint.UploadAsync(FilepathToUpload, destination!, overwrite: true);

        // Assert
        await Assert.ThrowsAsync<ArgumentNullException>(actDelegate);
    }

    #endregion

    #region Bytes

    [Fact]
    public async Task Upload_Bytes_ShouldCallCorrectUrl()
    {
        // Arrange
        var bytes = _fileSystem.File.ReadAllBytes(FilepathToUpload);
        var fileName = _fileSystem.Path.GetFileName(FilepathToUpload);

        var expectedResponse = new ApiResponse<FileStationUploadResponse>
        {
            Success = true,
            Data = _fixture.Create<FileStationUploadResponse>()
        };

        //expect certain request to server
        var request = _mockHttp.Expect(HttpMethod.Post, _baseUriWithApiPath.ToString())
            .WithQueryString(new Dictionary<string, string>
            {
                {"_sid", _session.Sid}
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
    public async Task Upload_Bytes_FileNameIsNullOrWhitespace_ShouldThrow(string? fileName)
    {
        // Arrange
        var bytes = await _fileSystem.File.ReadAllBytesAsync(FilepathToUpload);
        var fileStationUploadEndpoint = GetFileStationUploadEndpoint();

        // Act
        var actDelegate = () => fileStationUploadEndpoint.UploadAsync(bytes, fileName!, _destination, overwrite: true);

        // Assert
        await Assert.ThrowsAsync<ArgumentNullException>(actDelegate);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public async Task Upload_Bytes_DestinationIsNullOrWhitespace_ShouldThrow(string? destination)
    {
        // Arrange
        var bytes = _fileSystem.File.ReadAllBytes(FilepathToUpload);
        var fileName = _fileSystem.Path.GetFileName(FilepathToUpload);
        var fileStationUploadEndpoint = GetFileStationUploadEndpoint();

        // Act
        var actDelegate = () => fileStationUploadEndpoint.UploadAsync(bytes, fileName, destination!, overwrite: true);

        // Assert
        await Assert.ThrowsAsync<ArgumentNullException>(actDelegate);
    }

    [Fact]
    public async Task Upload_Bytes_BytesAreNull_ShouldThrow()
    {
        // Arrange
        var fileName = _fileSystem.Path.GetFileName(FilepathToUpload);
        var fileStationUploadEndpoint = GetFileStationUploadEndpoint();

        // Act
        var actDelegate = () => fileStationUploadEndpoint.UploadAsync(null!, fileName, _destination, overwrite: true);

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
        var request = _mockHttp.Expect(HttpMethod.Post, _baseUriWithApiPath.ToString())
            .WithQueryString(new Dictionary<string, string>
            {
                {"_sid", _session.Sid}
            });

        request.Respond(HttpStatusCode.OK, JsonContent.Create(expectedResponse));

        var fileStationUploadEndpoint = GetFileStationUploadEndpoint();

        // Act
        var actDelegate = () => fileStationUploadEndpoint.UploadAsync(FilepathToUpload, _destination, overwrite: true);

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

        var expectedRequestContentApiVersion = $"Content-Disposition: form-data; name=\"version\"\r\n\r\n{apiVersion}"; //do not use Environment.NewLine because of macOS
        var expectedRequestContentOverwrite = $"Content-Disposition: form-data; name=\"overwrite\"\r\n\r\n{overwriteValue}";

        //uses raw string literals, but seems to break tests in CI
        //var expectedRequestContentApiVersion = $"""
        //    Content-Disposition: form-data; name="version"

        //    {apiVersion}
        //    """;
        //var expectedRequestContentOverwrite = $"""
        //    Content-Disposition: form-data; name="overwrite"

        //    {overwriteValue}
        //    """;

        var expectedResponse = new ApiResponse<FileStationUploadResponse>
        {
            Success = true,
            Data = _fixture.Create<FileStationUploadResponse>()
        };

        //expect certain request to server
        var request = _mockHttp.Expect(HttpMethod.Post, _baseUriWithApiPath.ToString())
            .WithPartialContent(expectedRequestContentApiVersion)
            .WithPartialContent(expectedRequestContentOverwrite);

        request.Respond(HttpStatusCode.OK, JsonContent.Create(expectedResponse));

        var fileStationUploadEndpoint = GetFileStationUploadEndpoint(apiInfo);

        // Act
        var result = await fileStationUploadEndpoint.UploadAsync(FilepathToUpload, _destination, overwrite);

        // Assert
        result.Should().BeEquivalentTo(expectedResponse.Data);
    }
}