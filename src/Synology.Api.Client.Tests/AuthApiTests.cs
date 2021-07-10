using System;
using AutoFixture;
using Flurl.Http.Testing;
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
        private readonly HttpTest _httpTest;
        private readonly AuthApi _authApi;
        private readonly IApiInfo _apiInfo;

        public AuthApiTests(SynologyFixture synologyFixture)
        {
            _fixture = new Fixture();
            _httpTest = new HttpTest();
            _synologyFixture = synologyFixture;
            _apiInfo = _synologyFixture.ApisInfo.FileStationUploadApi;

            _authApi = new AuthApi(
                synologyFixture.SynologyHttpClient,
                _apiInfo);
        }

        [Fact]
        public async void AuthApi_Login_CallsCorrectUrl()
        {
            // arrange
            var username = _fixture.Create<string>();
            var password = _fixture.Create<string>();
            var otpCode = _fixture.Create<string>();

            var expectedResponse = new ApiResponse<LoginResponse>
            {
                Success = true,
                Data = _fixture.Create<LoginResponse>()
            };

            _httpTest.RespondWithJson(expectedResponse);

            // act
            await _authApi.LoginAsync(username, password, otpCode);

            // assert
            _httpTest
                .ShouldHaveCalled($"{_synologyFixture.BaseUrl}/{_apiInfo.Path}*")
                .WithQueryParams(new
                {
                    api = _apiInfo.Name,
                    version = _apiInfo.Version,
                    method = "login",
                    account = username,
                    passwd = password,
                    session = _apiInfo.SessionName,
                    format = "sid",
                    otp_code = otpCode
                });
        }

        public void Dispose()
        {
            _httpTest.Dispose();
        }
    }
}
