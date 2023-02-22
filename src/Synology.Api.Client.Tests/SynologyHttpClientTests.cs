using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Flurl.Http.Testing;
using Synology.Api.Client.ApiDescription;
using Synology.Api.Client.Exceptions;
using Synology.Api.Client.Shared.Models;
using Synology.Api.Client.Tests.Fixtures;
using Xunit;

namespace Synology.Api.Client.Tests
{
    public class SynologyHttpClientTests : IClassFixture<SynologyFixture>, IDisposable
    {
        private readonly SynologyFixture _synologyFixture;
        private readonly Fixture _fixture;
        private readonly HttpTest _httpTest;
        private readonly IApiInfo _fileStationCopyMoveApiInfo;

        public SynologyHttpClientTests(SynologyFixture synologyFixture)
        {
            _fixture = new Fixture();
            _httpTest = new HttpTest();
            _synologyFixture = synologyFixture;
            _fileStationCopyMoveApiInfo = _synologyFixture.ApisInfo.FileStationCopyMoveApi;
        }

        [Fact]
        public async Task GetAsync_HasAdditionalErrors_ShouldReturnAdditionalErrors()
        {
            // Arrange
            var apiMethod = _fixture.Create<string>();
            var errorPath = _fixture.Create<string>();
            var additionalErrorCode = 408;

            var expectedResponse = new BaseApiResponse
            {
                Success = false,
                Error = new Error
                {
                    Code = 1002,
                    Errors = new List<Shared.Models.Errors>
                    {
                        new Shared.Models.Errors
                        {
                            Code = additionalErrorCode,
                            Path = errorPath
                        }
                    }
                }
            };

            _httpTest.RespondWithJson(expectedResponse);

            // Act
            var synologyApiException = await Assert.ThrowsAsync<SynologyApiException>(() => _synologyFixture.SynologyHttpClient.GetAsync<BaseApiResponse>(_fileStationCopyMoveApiInfo, apiMethod, new { }));

            // Assert
            synologyApiException.Data.Count.Should().BeGreaterThan(0);
        }

        public void Dispose()
        {
            _httpTest.Dispose();
        }
    }
}
