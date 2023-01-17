using FluentAssertions;
using Synology.Api.Client.Apis.DownloadStation.Info.Models;
using Synology.Api.Client.Integration.Tests.Fixtures;
using Xunit;

namespace Synology.Api.Client.Integration.Tests
{
    public class DownloadStationApiTests : IClassFixture<SynologyFixture>
    {
        private readonly SynologyFixture _fixture;

        public DownloadStationApiTests(SynologyFixture fixture)
        {
            _fixture = fixture;

            _fixture.LoginAsync().Wait();
        }

        [Fact]
        public async void DownloadStationApi_DownloadStation_ListTasks()
        {
            // arrange && act
            var listResponse = await _fixture
                .Client
                .DownloadStationApi()
                .TaskEndpoint()
                .ListAsync();

            // assert
            listResponse.Should().NotBeNull();
        }

        [Fact]
        public async void DownloadStationApi_DownloadStation_Info()
        {
            var infoResponse = await _fixture
                .Client
                .DownloadStationApi()
                .InfoEndpoint()
                .InfoAsync();

            infoResponse.Should().NotBeNull();
        }

        [Fact]
        public async void DownloadStationApi_DownloadStation_GetConfig()
        {
            var getConfigResponse = await _fixture
                .Client
                .DownloadStationApi()
                .InfoEndpoint()
                .GetConfigAsync();

            getConfigResponse.Should().NotBeNull();
        }

        [Fact]
        public async void DownloadStationApi_DownloadStation_SetServerConfig()
        {
            var getConfigResponse = await _fixture
                .Client
                .DownloadStationApi()
                .InfoEndpoint()
                .GetConfigAsync();
            
            var config = new DownloadStationServerConfig(
                btMaxDownloadSpeed: 100,
                btMaxUploadSpeed: 100,
                emulEnable: false,
                emulMaxDownloadSpeed: 200,
                emulMaxUploadSpeed: 200,
                ftpMaxDownloadSpeed: 300,
                httpMaxDownloadSpeed: 400,
                nzbMaxDownloadSpeed: 500,
                unzipServiceEnable: true,
                defaultDestination: "/home/temp"
            );
            
            var setConfigResponse = await _fixture
                .Client
                .DownloadStationApi()
                .InfoEndpoint()
                .SetServerConfigAsync(new DownloadStationServerConfig(emulEnable: true));

            setConfigResponse.Should().NotBeNull();
        }
    }
}
