using Xunit;

namespace Synology.Api.Client.Integration.Tests.Fixtures;

public class SynologyFixture : IAsyncLifetime
{
    public SynologyFixture()
    {
        // TODO: Add your details below before running the integration tests
        DsmUrl = "http://192.168.0.1:5001";
        Username = "username";
        Password = "password";
        OtpCode = "";

        Client = new SynologyClient(DsmUrl);
    }
    
    public async Task InitializeAsync()
    {
        await Client.LoginAsync(Username, Password, OtpCode);
    }

    public async Task DisposeAsync()
    {
        await Client.LogoutAsync();
    }

    public string DsmUrl { get; }

    public string Username { get; }

    public string Password { get; }

    public string OtpCode { get; }

    public ISynologyClient Client { get; }
}