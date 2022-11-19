using System.Threading.Tasks;
using Synology.Api.Client.Apis.FileStation.CreateFolder.Models;
using Synology.Api.Client.Shared.Models;

namespace Synology.Api.Client.Apis.FileStation.CreateFolder
{
    public interface IFileStationCreateFolderEndpoint
    {
        Task<FileStationCreateFolderCreateResponse> CreateAsync(string[] paths, bool createParentFolders);
    }
}
