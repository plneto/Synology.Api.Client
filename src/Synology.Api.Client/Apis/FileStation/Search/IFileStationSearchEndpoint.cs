using System.Threading.Tasks;
using Synology.Api.Client.Apis.FileStation.Search.Models;

namespace Synology.Api.Client.Apis.FileStation.Search
{
    public interface IFileStationSearchEndpoint
    {
        /// <summary>
        /// Start to search files according to given criteria. If more than one criterion is given in different parameters,
        /// searched files match all these criteria.
        /// </summary>
        /// 
        /// <example>
        /// This sample shows how to call the <see cref="StartAsync"/> method.
        /// <code>
        /// var searchResult = await client.FileStationApi().SearchEndpoint().SearchAsync(request);
        /// </code>
        /// </example>
        /// 
        /// <param name="request">The criteria used in the search request.</param>
        /// <returns>Returns <see cref="FileStationSearchStartResponse"/></returns>
        Task<FileStationSearchStartResponse> StartAsync(FileStationSearchStartRequest request);

        /// <summary>
        /// List matched files in a search temporary database. You can check the finished value in response
        /// to know if the search operation is processing or has been finished.
        /// </summary>
        /// <param name="taskId">A unique ID for the search task which is obtained from start method.</param>
        /// <param name="offset">Optional. Specify how many matched files are skipped before beginning to return listed matched files.</param>
        /// <param name="limit">Optional. Number of matched files requested. -1 indicates to list all matched files. 0 indicates to list nothing.</param>
        /// <returns>The list of files for given task id</returns>
        Task<FileStationSearchListResponse> ListAsync(string taskId, int offset = 0, int limit = -1);
    }
}
