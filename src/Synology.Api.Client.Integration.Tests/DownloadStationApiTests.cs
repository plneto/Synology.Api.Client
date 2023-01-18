using System.Threading.Tasks;
using FluentAssertions;
using Synology.Api.Client.Apis.DownloadStation.Task.Models;
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

        //ToDo добавить существующую ссылку
        [Fact]
        public async Task DownloadStationApi_DownloadStation_CreateTaskWithLogin()
        {
            // arrange && act
            var request = new DownloadStationTaskCreateRequest("ftps://192.0.0.1:21/test/test.zip");
            
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
            var request = new DownloadStationTaskCreateRequest("ftps://192.0.0.1:21/test/test.zip", username: "",
                password: "");
            
            var createResponse = await fixture
                .Client
                .DownloadStationApi()
                .TaskEndpoint()
                .CreateAsync(request);
            
            // assert
            createResponse.Should().NotBeNull();
        }
    }
}
