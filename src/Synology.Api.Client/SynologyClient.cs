﻿using System;
using System.Net.Http;
using System.Security;
using System.Threading.Tasks;
using Flurl.Http;
using Synology.Api.Client.ApiDescription;
using Synology.Api.Client.Apis;
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
        private readonly IFlurlClient _flurlClient;
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

            _flurlClient = new FlurlClient(httpClient)
            {
                BaseUrl = $"{dsmUrl.TrimEnd('/')}/webapi"
            };

            _flurlClient.AllowAnyHttpStatus();

            _synologyHttpClient = new SynologyHttpClient(_flurlClient);

            ApisInfo = new DefaultApisInfo();
        }

        public IApisInfo ApisInfo { get; set; }

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
    }
}
