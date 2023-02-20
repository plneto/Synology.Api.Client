using FluentAssertions;
using Synology.Api.Client.Integration.Tests.Fixtures;
using Xunit;

namespace Synology.Api.Client.Integration.Tests
{
    public class InfoApiTests : IClassFixture<SynologyFixture>
    {
        private readonly SynologyFixture _fixture;

        public InfoApiTests(SynologyFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async void InfoApi_Query_Success()
        {
            // arrange && act
            var response = await _fixture.Client.InfoApi().QueryAsync();

            // assert
            response.Should().NotBeNull();
        }
        
        [Fact]
        public void InfoApi_EnsureAllApisHaveAPathDefined_Success()
        {
            // arrange && act
            var apisInfo = _fixture.Client.ApisInfo;
            
            // assert
            apisInfo.InfoApi.Path.Should().NotBeNullOrWhiteSpace();
            apisInfo.AuthApi.Path.Should().NotBeNullOrWhiteSpace();
            apisInfo.DownloadStationTaskApi.Path.Should().NotBeNullOrWhiteSpace();
            apisInfo.FileStationCopyMoveApi.Path.Should().NotBeNullOrWhiteSpace();
            apisInfo.FileStationCreateFolderApi.Path.Should().NotBeNullOrWhiteSpace();
            apisInfo.FileStationExtractApi.Path.Should().NotBeNullOrWhiteSpace();
            apisInfo.FileStationListApi.Path.Should().NotBeNullOrWhiteSpace();
            apisInfo.FileStationUploadApi.Path.Should().NotBeNullOrWhiteSpace();
            apisInfo.FileStationSearchApi.Path.Should().NotBeNullOrWhiteSpace();
        }
    }
}