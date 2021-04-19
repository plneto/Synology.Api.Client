using System.Threading.Tasks;
using Synology.Api.Client.Apis.FileStation.Extract.Models;
using Synology.Api.Client.Shared.Models;

namespace Synology.Api.Client.Apis.FileStation.Extract
{
    public interface IFileStationExtractEndpoint
    {
        Task<FileStationExtractStartResponse> StartAsync(string filePath, string destination, bool overwrite);

        Task<FileStationExtractStatusResponse> GetStatusAsync(string taskId);

        Task<BaseApiResponse> StopAsync(string taskId);

        Task<FileStationExtractListResponse> ListFilesAsync(string filePath);
    }
}
