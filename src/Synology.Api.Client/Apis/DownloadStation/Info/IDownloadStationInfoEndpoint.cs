using System.Threading.Tasks;
using Synology.Api.Client.Apis.DownloadStation.Info.Models;

namespace Synology.Api.Client.Apis.DownloadStation.Info
{
    public interface IDownloadStationInfoEndpoint
    {
        Task<DownloadStationInfoResponse> InfoAsync();
        
        Task<DownloadStationServerConfig> GetConfigAsync();

        Task<DownloadStationServerConfig> SetServerConfigAsync(DownloadStationServerConfig config);
    }
}