using System.Threading.Tasks;
using Synology.Api.Client.Apis.FileStation.Upload.Models;

namespace Synology.Api.Client.Apis.FileStation.Upload
{
    public interface IFileStationUploadEndpoint
    {
        Task<FileStationUploadResponse> UploadAsync(string filePath, string destination);

        Task<FileStationUploadResponse> UploadAsync(byte[] bytes, string filename, string destination);
    }
}
