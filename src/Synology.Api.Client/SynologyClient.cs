using System;
using System.Net.Http;
using System.Security;
using System.Threading.Tasks;
using Synology.Api.Client.ApiDescription;
using Synology.Api.Client.Apis.Auth;
using Synology.Api.Client.Apis.DownloadStation;
using Synology.Api.Client.Apis.FileStation;
using Synology.Api.Client.Apis.Info;
using Synology.Api.Client.Constants;
using Synology.Api.Client.Session;

namespace Synology.Api.Client
{
    public class SynologyClient : ISynologyClient
    {
        private readonly ISynologyHttpClient _synologyHttpClient;

        public SynologyClient(string dsmUrl)
            : this(dsmUrl, new HttpClient())
        {
        }

        public SynologyClient(string dsmUrl, HttpClient httpClient)
        {
            if (string.IsNullOrWhiteSpace(dsmUrl))
            {
                throw new ArgumentNullException(nameof(dsmUrl));
            }

            httpClient.BaseAddress = new Uri($"{dsmUrl.TrimEnd('/')}/webapi");

            _synologyHttpClient = new SynologyHttpClient(httpClient);

            Task.Run(() => UpdateApisInfoAsync()).Wait();
        }

        public IApisInfo ApisInfo { get; set; } = new DefaultApisInfo();

        public ISynologySession Session { get; set; }

        public bool IsLoggedIn => Session != null && !string.IsNullOrWhiteSpace(Session.Sid);

        public IInfoEndpoint InfoApi()
        {
            return new InfoEndpoint(_synologyHttpClient, ApisInfo.InfoApi);
        }

        public IAuthApi AuthApi()
        {
            return new AuthApi(_synologyHttpClient, ApisInfo.AuthApi);
        }

        public IDownloadStationApi DownloadStationApi()
        {
            if (!IsLoggedIn)
            {
                throw new SecurityException(CustomErrorMessages.SessionNotAuthenticated);
            }

            return new DownloadStationApi(_synologyHttpClient, ApisInfo, Session);
        }

        public IFileStationApi FileStationApi()
        {
            if (!IsLoggedIn)
            {
                throw new SecurityException(CustomErrorMessages.SessionNotAuthenticated);
            }

            return new FileStationApi(_synologyHttpClient, ApisInfo, Session);
        }

        public async Task LoginAsync(
            string username,
            string password,
            string otpCode = "")
        {
            var loginResult = await AuthApi().LoginAsync(username, password, otpCode);

            Session = new SynologySession(loginResult.Sid);
        }

        public async Task LogoutAsync()
        {
            if (!IsLoggedIn)
            {
                return;
            }

            await AuthApi().LogoutAsync(Session.Sid);

            Session = null;
        }

        /// <summary>
        /// Updates the API descriptions using the response from the InfoApi endpoint.
        /// </summary>
        private async Task UpdateApisInfoAsync()
        {
            var infoQueryResponse = await InfoApi().QueryAsync();

            ApisInfo = DefaultApisInfo.FromInfoQueryResponse(ApisInfo, infoQueryResponse);
        }
    }
}
