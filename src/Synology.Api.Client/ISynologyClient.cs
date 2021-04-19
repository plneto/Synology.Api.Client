using System.Threading.Tasks;
using Synology.Api.Client.ApiDescription;
using Synology.Api.Client.Apis.Auth;
using Synology.Api.Client.Apis.DownloadStation;
using Synology.Api.Client.Apis.FileStation;
using Synology.Api.Client.Apis.Info;
using Synology.Api.Client.Session;

namespace Synology.Api.Client
{
    public interface ISynologyClient
    {
        IApisInfo ApisInfo { get; set; }

        ISynologySession Session { get; set; }

        bool IsLoggedIn { get; }

        IInfoEndpoint InfoApi();

        IAuthApi AuthApi();

        IDownloadStationApi DownloadStationApi();

        IFileStationApi FileStationApi();

        Task LoginAsync(string username, string password, string otpCode = "");

        Task LogoutAsync();
    }
}
