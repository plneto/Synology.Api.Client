using FluentAssertions;
using Synology.Api.Client.Integration.Tests.Fixtures;
using Xunit;

namespace Synology.Api.Client.Integration.Tests
{
    public class AuthApiTests : IClassFixture<SynologyFixture>
    {
        private readonly SynologyFixture _fixture;

        public AuthApiTests(SynologyFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async void AuthApi_Login_Success()
        {
            // arrange && act
            var loginResult = await _fixture.Client.AuthApi().LoginAsync(
                _fixture.Username,
                _fixture.Password,
                _fixture.OtpCode
            );

            // assert
            loginResult.Sid.Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public async void AuthApi_LoginAndLogout_Success()
        {
            // arrange && act
            var loginResult = await _fixture.Client.AuthApi().LoginAsync(
                _fixture.Username,
                _fixture.Password,
                _fixture.OtpCode
            );

            var logoutResult = await _fixture.Client.AuthApi().LogoutAsync(loginResult.Sid);

            // assert
            loginResult.Sid.Should().NotBeNullOrWhiteSpace();

            logoutResult.Success.Should().BeTrue();
        }
    }
}