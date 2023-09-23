using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Synology.Api.Client.ApiDescription;
using Synology.Api.Client.Apis.Auth.Models;
using Synology.Api.Client.Shared.Models;

namespace Synology.Api.Client.Apis.Auth
{
    public class AuthApi : IAuthApi
    {
        private readonly ISynologyHttpClient _synologyHttpClient;
        private readonly IApiInfo _apiInfo;

        public AuthApi(ISynologyHttpClient synologyHttpClient, IApiInfo apiInfo)
        {
            _synologyHttpClient = synologyHttpClient;
            _apiInfo = apiInfo;
        }

        public Task<LoginResponse> LoginAsync(string username, string password, string? otpCode = null)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentNullException(nameof(username));
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentNullException(nameof(password));
            }

            var queryParams = new Dictionary<string, string?>
            {
                { "account", username },
                { "passwd", password },
                { "format", "sid" }
            };

            if (!string.IsNullOrWhiteSpace(otpCode))
            {
                queryParams.Add("otp_code", otpCode);
            }

            return _synologyHttpClient.GetAsync<LoginResponse>(_apiInfo, "login", queryParams);
        }

        public Task<BaseApiResponse> LogoutAsync(string sid)
        {
            if (string.IsNullOrWhiteSpace(sid))
            {
                throw new ArgumentNullException(nameof(sid));
            }

            return _synologyHttpClient.GetAsync<BaseApiResponse>(
                _apiInfo, "logout", 
                new Dictionary<string, string?> { { "_sid", sid } });
        }
    }
}
