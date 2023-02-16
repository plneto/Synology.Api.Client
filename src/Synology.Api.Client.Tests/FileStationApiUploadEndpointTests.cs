//using System;
//using System.Collections.Generic;
//using System.IO.Abstractions.TestingHelpers;
//using System.Threading.Tasks;
//using AutoFixture;
//using FluentAssertions;
//using Flurl.Http.Testing;
//using Synology.Api.Client.ApiDescription;
//using Synology.Api.Client.Apis.FileStation.Upload;
//using Synology.Api.Client.Apis.FileStation.Upload.Models;
//using Synology.Api.Client.Errors;
//using Synology.Api.Client.Exceptions;
//using Synology.Api.Client.Session;
//using Synology.Api.Client.Shared.Models;
//using Synology.Api.Client.Tests.Fixtures;
//using Xunit;

//namespace Synology.Api.Client.Tests
//{
//    public class FileStationApiUploadEndpointTests : IClassFixture<SynologyFixture>, IDisposable
//    {
//        private readonly SynologyFixture _synologyFixture;
//        private readonly Fixture _fixture;
//        private readonly HttpTest _httpTest;
//        private readonly FileStationUploadEndpoint _fileStationUploadEndpoint;
//        private readonly IApiInfo _apiInfo;

//        private const string TestFilePath = @"c:\myfile.txt";

//        public FileStationApiUploadEndpointTests(SynologyFixture synologyFixture)
//        {
//            _fixture = new Fixture();
//            _httpTest = new HttpTest();
//            _synologyFixture = synologyFixture;
//            _apiInfo = _synologyFixture.ApisInfo.FileStationUploadApi;

//            var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
//            {
//                { TestFilePath, new MockFileData("Test file") }
//            });

//            _fileStationUploadEndpoint = new FileStationUploadEndpoint(
//                synologyFixture.SynologyHttpClient,
//                _apiInfo,
//                new SynologySession(_fixture.Create<string>()),
//                fileSystem);
//        }

//        [Fact]
//        public async void UploadEndpoint_Upload_CallsCorrectUrl()
//        {
//            // arrange
//            var destination = _fixture.Create<string>();

//            var expectedResponse = new ApiResponse<FileStationUploadResponse>
//            {
//                Success = true,
//                Data = _fixture.Create<FileStationUploadResponse>()
//            };

//            _httpTest.RespondWithJson(expectedResponse);

//            // act
//            await _fileStationUploadEndpoint.UploadAsync(TestFilePath, destination, true);

//            // assert
//            _httpTest
//                .ShouldHaveCalled($"{_synologyFixture.BaseUrl}/{_apiInfo.Path}*");
//        }

//        [Fact]
//        public void UploadEndpoint_UploadFilePathNotFound_DisplaysCorrectError()
//        {
//            // arrange
//            var destination = _fixture.Create<string>();
//            var notFoundErrorCode = 408;

//            var expectedResponse = new ApiResponse<FileStationUploadResponse>
//            {
//                Success = false,
//                Error = new Error
//                {
//                    Code = notFoundErrorCode
//                }
//            };

//            _httpTest.RespondWithJson(expectedResponse);

//            // act
//            Func<Task> action = async () => await _fileStationUploadEndpoint.UploadAsync(TestFilePath, destination, true);

//            // assert
//            action
//                .Should()
//                .ThrowAsync<SynologyApiException>().Result
//                .And
//                .ErrorDescription
//                .Should()
//                .BeEquivalentTo(ErrorMessages.FileStationApiErrors.GetValueOrDefault(notFoundErrorCode));
//        }

//        public void Dispose()
//        {
//            _httpTest.Dispose();
//        }
//    }
//}
