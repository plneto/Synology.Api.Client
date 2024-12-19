using FluentAssertions;
using Synology.Api.Client.Apis.DownloadStation.Info.Models;
using Synology.Api.Client.Apis.DownloadStation.Task.Models;
using Synology.Api.Client.Integration.Tests.Fixtures;
using Xunit;

namespace Synology.Api.Client.Integration.Tests;

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
    public async Task DownloadStationApi_DownloadStation_CreateTaskByUriWithLogin()
    {
        // arrange && act
        var uri = ""; //file uri
        var request = new DownloadStationTaskCreateRequest(uri);

        var createResponse = await _fixture
            .Client
            .DownloadStationApi()
            .TaskEndpoint()
            .CreateAsync(request);

        // assert
        createResponse.Should().NotBeNull();
    }

    [Fact]
    public async Task DownloadStationApi_DownloadStation_DeleteTask()
    {
        var request = new DownloadStationTaskCreateRequest(
            uri: "magnet:?xt=urn:btih:fef84077088ca87ffd8afd644d0ef957d96243c3&dn=archlinux-2023.01.01-x86_64.iso");

        var listTaskBefore = await _fixture
            .Client
            .DownloadStationApi()
            .TaskEndpoint()
            .ListAsync();

        await _fixture
            .Client
            .DownloadStationApi()
            .TaskEndpoint()
            .CreateAsync(request);

        var listTaskAfterAdd = await _fixture
            .Client
            .DownloadStationApi()
            .TaskEndpoint()
            .ListAsync();

        var delRequest = new DownloadStationTaskDeleteRequest
        {
            Ids = new List<string> { listTaskAfterAdd.Task.Last().Id },
            ForceComplete = false
        };

        await _fixture
            .Client
            .DownloadStationApi()
            .TaskEndpoint()
            .DeleteAsync(delRequest);

        var listTaskAfter = await _fixture
            .Client
            .DownloadStationApi()
            .TaskEndpoint()
            .ListAsync();

        Assert.Equal(listTaskBefore.Total, listTaskAfter.Total);
    }

    [Fact]
    public async Task DownloadStationApi_DownloadStation_ResumeTasks()
    {
        var request = new DownloadStationTaskCreateRequest(
            uri: "magnet:?xt=urn:btih:fef84077088ca87ffd8afd644d0ef957d96243c3&dn=archlinux-2023.01.01-x86_64.iso");

        await _fixture
            .Client
            .DownloadStationApi()
            .TaskEndpoint()
            .CreateAsync(request);

        var listTask = await _fixture
            .Client
            .DownloadStationApi()
            .TaskEndpoint()
            .ListAsync();

        var id = listTask.Task.Last().Id;

        await _fixture
            .Client
            .DownloadStationApi()
            .TaskEndpoint()
            .PauseAsync(id);

        var resumeResponse = await _fixture
            .Client
            .DownloadStationApi()
            .TaskEndpoint()
            .ResumeAsync(id);

        resumeResponse.Should().NotBeNull();

        var delRequest = new DownloadStationTaskDeleteRequest
        {
            Ids = new List<string> { id },
            ForceComplete = false
        };

        await _fixture
            .Client
            .DownloadStationApi()
            .TaskEndpoint()
            .DeleteAsync(delRequest);
    }

    [Fact]
    public async Task DownloadStationApi_DownloadStation_PauseTask()
    {
        var request = new DownloadStationTaskCreateRequest(
            uri: "magnet:?xt=urn:btih:fef84077088ca87ffd8afd644d0ef957d96243c3&dn=archlinux-2023.01.01-x86_64.iso");

        await _fixture
            .Client
            .DownloadStationApi()
            .TaskEndpoint()
            .CreateAsync(request);

        var listTask = await _fixture
            .Client
            .DownloadStationApi()
            .TaskEndpoint()
            .ListAsync();

        var pauseResponse = _fixture
            .Client
            .DownloadStationApi()
            .TaskEndpoint()
            .PauseAsync(listTask.Task.Last().Id);

        pauseResponse.Should().NotBeNull();
    }

    [Fact]
    public async Task DownloadStationApi_DownloadStation_Info()
    {
        var infoResponse = await _fixture
            .Client
            .DownloadStationApi()
            .InfoEndpoint()
            .InfoAsync();

        infoResponse.Should().NotBeNull();
    }

    [Fact]
    public async Task DownloadStationApi_DownloadStation_GetConfig()
    {
        var getConfigResponse = await _fixture
            .Client
            .DownloadStationApi()
            .InfoEndpoint()
            .GetConfigAsync();

        getConfigResponse.Should().NotBeNull();
    }

    [Fact]
    public async Task DownloadStationApi_DownloadStation_SetServerConfig()
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

        await _fixture
            .Client
            .DownloadStationApi()
            .InfoEndpoint()
            .SetServerConfigAsync(config);

        // Edit emulDefaultDestination
        await _fixture
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
        await _fixture
            .Client
            .DownloadStationApi()
            .InfoEndpoint()
            .SetServerConfigAsync(getConfigResponse);

        getConfigResponseNew.Should().BeEquivalentTo(config);
    }
}