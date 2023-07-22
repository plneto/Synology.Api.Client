using System.Threading.Tasks;
using Synology.Api.Client.Apis.DownloadStation.Info.Models;
using Synology.Api.Client.Shared.Models;

namespace Synology.Api.Client.Apis.DownloadStation.Info
{
    public interface IDownloadStationInfoEndpoint
    {
        Task<DownloadStationInfoResponse> InfoAsync();
        
        Task<DownloadStationServerConfig> GetConfigAsync();

        /// <summary>
        /// Download service config can only be changed by a admin only.
        /// 
        /// You can set one or more parameters in a configuration object.
        /// By default, the rest of the parameters will not change.
        /// Parameter EmulDefaultDestination changes only when the service Emule is enabled
        /// </summary>
        /// <param name="config">Object with parameters to changed</param>
        /// <returns>Returns on success null</returns>
        Task<BaseApiResponse> SetServerConfigAsync(DownloadStationServerConfig config);
    }
}