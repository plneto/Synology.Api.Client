using System.Threading.Tasks;
using FluentAssertions;
using Synology.Api.Client.Apis.DownloadStation.Task.Models;
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
        public async Task DownloadStationApi_DownloadStation_ListTasks()
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
        public async Task DownloadStationApi_DownloadStation_CreateTaskWithLogin()
        {
            // arrange && act
            var request = new DownloadStationTaskCreateRequest("");
            
            var createResponse = await _fixture
                .Client
                .DownloadStationApi()
                .TaskEndpoint()
                .CreateAsync(request);
            
            // assert
            createResponse.Should().NotBeNull();
        }
        
        //ToDo указать ссылку на файл или логин и пароль
        [Fact]
        public async Task DownloadStationApi_DownloadStation_CreateTasksWithoutLogin()
        {
            // arrange && act
            var fixture = new SynologyFixture();
            var request = new DownloadStationTaskCreateRequest("", 
                username: "", password: "");
            
            var createResponse = await fixture
                .Client
                .DownloadStationApi()
                .TaskEndpoint()
                .CreateAsync(request);
            
            // assert
            createResponse.Should().NotBeNull();
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
            
            // Changing current config DownloadStation
            var config = new DownloadStationServerConfig(
                btMaxDownloadSpeed: getConfigResponse.BtMaxDownloadSpeed + 100,
                btMaxUploadSpeed: getConfigResponse.BtMaxUploadSpeed + 100,
                emulEnable: !getConfigResponse.EmulEnable,
                emulMaxDownloadSpeed: getConfigResponse.EmulMaxDownloadSpeed + 100,
                emulMaxUploadSpeed: getConfigResponse.EmulMaxUploadSpeed + 100,
                ftpMaxDownloadSpeed: getConfigResponse.FtpMaxDownloadSpeed + 100,
                httpMaxDownloadSpeed: getConfigResponse.HttpMaxDownloadSpeed + 100,
                nzbMaxDownloadSpeed: getConfigResponse.NzbMaxDownloadSpeed + 100,
                unzipServiceEnable: !getConfigResponse.UnzipServiceEnable,
                defaultDestination: getConfigResponse.DefaultDestination + "/tmp"
            );

            var setConfigResponse = await _fixture
                .Client
                .DownloadStationApi()
                .InfoEndpoint()
                .SetServerConfigAsync(config);
            
            // Edit emulDefaultDestination
            setConfigResponse = await _fixture
                .Client
                .DownloadStationApi()
                .InfoEndpoint()
                .SetServerConfigAsync(
                    new DownloadStationServerConfig(
                        emulDefaultDestination: config.DefaultDestination
                        )
                    );

            // Check that the config has changed 
            var getConfigResponseNew = await _fixture
                .Client
                .DownloadStationApi()
                .InfoEndpoint()
                .GetConfigAsync();
            
            // Changing config to original state
            setConfigResponse = await _fixture
                .Client
                .DownloadStationApi()
                .InfoEndpoint()
                .SetServerConfigAsync(getConfigResponse);
            
            getConfigResponseNew.Should().BeEquivalentTo(config);
        }
    }
}
