using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using RichardSzalay.MockHttp;
using Synology.Api.Client.ApiDescription;
using Synology.Api.Client.Apis.Auth;
using Synology.Api.Client.Apis.Auth.Models;
using Synology.Api.Client.Shared.Models;
using Synology.Api.Client.Tests.Fixtures;
using Xunit;

namespace Synology.Api.Client.Tests
{
    public class AuthApiTests : IClassFixture<SynologyFixture>, IDisposable
    {
        private readonly SynologyFixture _synologyFixture;
        private readonly Fixture _fixture;
        private readonly IApiInfo _apiInfo;
        private readonly MockHttpMessageHandler _mockHtttp;
        private readonly Uri _baseUriWithApiPath;

        public AuthApiTests(SynologyFixture synologyFixture)
        {
            _synologyFixture = synologyFixture;
            _fixture = new Fixture();
            _apiInfo = _synologyFixture.ApisInfo.AuthApi;
            _mockHtttp = new MockHttpMessageHandler();

            _baseUriWithApiPath = _synologyFixture.GetBaseUriWithPath(_apiInfo.Path);
        }

        public void Dispose()
        {
            _mockHtttp.Dispose();
        }

        #region helper methods

        private IAuthApi GetAuthApi()
        {
            var httpClient = _mockHtttp.ToHttpClient();
            httpClient.BaseAddress = _synologyFixture.BaseUri;

            var synologyHttpClient = new SynologyHttpClient(httpClient);
            var authApi = new AuthApi(synologyHttpClient, _apiInfo);
            return authApi;
        }

        #endregion

        [Fact]
        public async void Login_ShouldCallCorrectUrl()
        {
            // Arrange
            var username = _fixture.Create<string>();
            var password = _fixture.Create<string>();
            var otpCode = _fixture.Create<string>();

            var expectedResponse = new ApiResponse<LoginResponse>
            {
                Success = true,
                Data = _fixture.Create<LoginResponse>()
            };

            //expect certain request to server
            var request = _mockHtttp.Expect(System.Net.Http.HttpMethod.Get, _baseUriWithApiPath.ToString())
                 .WithQueryString(new Dictionary<string, string>
                 {
                    { "api" , _apiInfo.Name },
                    { "version" , _apiInfo.Version.ToString() },
                    { "method" , "login" },
                    { "account" , username },
                    { "passwd" , password },
                    { "format" , "sid" },
                    { "otp_code" , otpCode }
                 });

            if (!string.IsNullOrWhiteSpace(_apiInfo.SessionName))
            {
                request = request.WithQueryString("session", _apiInfo.SessionName);
            }

            request.Respond(HttpStatusCode.OK, JsonContent.Create(expectedResponse));

            //expect certain response from server
            //_mockHtttp.When(System.Net.Http.HttpMethod.Get, _baseUriWithApiPath.ToString())
            //    .Respond(HttpStatusCode.OK, JsonContent.Create(expectedResponse));

            var authApi = GetAuthApi();

            // Act
            var result = await authApi.LoginAsync(username, password, otpCode);

            // Assert
            result.Should().BeEquivalentTo(expectedResponse.Data);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task Login_UsernameIsNullOrWhitespace_ShouldThrow(string username)
        {
            // Arrange
            var password = _fixture.Create<string>();
            var otpCode = _fixture.Create<string>();

            var authApi = GetAuthApi();

            // Act
            var actDelegate = () => authApi.LoginAsync(username, password, otpCode);

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(actDelegate);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task Login_PasswordIsNullOrWhitespace_ShouldThrow(string password)
        {
            // Arrange
            var username = _fixture.Create<string>();
            var otpCode = _fixture.Create<string>();

            var authApi = GetAuthApi();

            // Act
            var actDelegate = () => authApi.LoginAsync(username, password, otpCode);

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(actDelegate);
        }

        [Fact]
        public async void Logout_CallsCorrectUrl()
        {
            // Arrange
            var sid = _fixture.Create<string>();

            var expectedResponse = new BaseApiResponse
            {
                Success = true,
                Error = null
            };

            //expect certain request to server
            var request = _mockHtttp.Expect(System.Net.Http.HttpMethod.Get, _baseUriWithApiPath.ToString())
                 .WithQueryString(new Dictionary<string, string>
                 {
                    { "api" , _apiInfo.Name },
                    { "version" , _apiInfo.Version.ToString() },
                    { "method" , "logout" },
                    { "_sid" , sid }
                 });

            if (!string.IsNullOrWhiteSpace(_apiInfo.SessionName))
            {
                request = request.WithQueryString("session", _apiInfo.SessionName);
            }

            request.Respond(HttpStatusCode.OK, JsonContent.Create(expectedResponse));

            var authApi = GetAuthApi();

            // Act
            var result = await authApi.LogoutAsync(sid);

            // Assert
            result.Should().BeEquivalentTo(expectedResponse);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task Logout_SidIsNullOrWhitespace_ShouldThrow(string sid)
        {
            // Arrange
            var authApi = GetAuthApi();

            // Act
            var actDelegate = () => authApi.LogoutAsync(sid);

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(actDelegate);
        }
    }
}
