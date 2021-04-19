using System.Threading.Tasks;
using Synology.Api.Client.Apis.FileStation.CopyMove.Models;
using Synology.Api.Client.Shared.Models;

namespace Synology.Api.Client.Apis.FileStation.CopyMove
{
    public interface IFileStationCopyMoveEndpoint
    {
        Task<FileStationCopyMoveStartResponse> StartCopyAsync(string[] paths, string destination, bool overwrite);

        Task<FileStationCopyMoveStartResponse> StartMoveAsync(string[] paths, string destination, bool overwrite);

        Task<FileStationCopyMoveStatusResponse> GetStatusAsync(string taskId);

        Task<BaseApiResponse> StopAsync(string taskId);
    }
}
