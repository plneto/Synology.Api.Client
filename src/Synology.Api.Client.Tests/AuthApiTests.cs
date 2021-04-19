using System;
using AutoFixture;
using Flurl.Http.Testing;
using Synology.Api.Client.ApiDescription;
using Xunit;

namespace Synology.Api.Client.Tests
{
    public class AuthApiTests : IDisposable
    {
        private const string dsmUrl = "http://dsm-url.com";

        private readonly Fixture _fixture;
        private readonly HttpTest _httpTest;
        private readonly ISynologyClient _synologyClient;
        private readonly IApiInfo _apiInfo;

        public AuthApiTests()
        {
            _fixture = new Fixture();
            _httpTest = new HttpTest();
            _synologyClient = new SynologyClient(dsmUrl);

            _apiInfo = _synologyClient.ApisInfo.AuthApi;
        }

        [Fact]
        public async void AuthApi_Login_CallsCorrectUrl()
        {
            // arrange
            var username = _fixture.Create<string>();
            var password = _fixture.Create<string>();
            var otpCode = _fixture.Create<string>();

            // act
            await _synologyClient.AuthApi().LoginAsync(username, password, otpCode);

            // assert
            _httpTest
                .ShouldHaveCalled($"{dsmUrl}/webapi/{_apiInfo.Path}*")
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
