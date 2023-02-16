using System.Threading.Tasks;
using Synology.Api.Client.Apis.FileStation.List.Models;

namespace Synology.Api.Client.Apis.FileStation.List
{
    public interface IFileStationListEndpoint
    {
        /// <summary>
        /// Enumerate files in a given folder.
        /// </summary>
        /// <param name="fileStationListRequest">A <see cref="FileStationListRequest"/></param>
        /// <returns>Returns <see cref="FileStationListResponse"/></returns>
        Task<FileStationListResponse> ListAsync(FileStationListRequest fileStationListRequest);

        /// <summary>
        /// List all shared folders, enumerate files in a shared folder, and get detailed file information.
        /// </summary>
        /// <returns>Returns <see cref="FileStationListShareResponse"/></returns>
        Task<FileStationListShareResponse> ListSharesAsync();
    }
}
