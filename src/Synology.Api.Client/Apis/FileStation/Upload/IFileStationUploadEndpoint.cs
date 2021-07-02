using System.Threading.Tasks;
using Synology.Api.Client.Apis.FileStation.Upload.Models;

namespace Synology.Api.Client.Apis.FileStation.Upload
{
    public interface IFileStationUploadEndpoint
    {
        /// <summary>
        /// Upload a file 
        /// </summary>
        /// 
        /// <example>
        /// This sample shows how to call the <see cref="UploadAsync"/> method.
        /// <code>
        /// var uploadResult = await client.FileStationApi().UploadEndpoint().UploadAsync("C:\myfile.txt", "/my_share/docs", true);
        /// </code>
        /// </example>
        /// 
        /// <param name="filePath">The path of the file to be uploaded.</param>
        /// <param name="destination">A destination folder path starting with a shared folder to which files can be uploaded.</param>
        /// <param name="overwrite">Overwrite the destination file if one exists, or skip the upload.</param>
        /// <returns>Returns <see cref="FileStationUploadResponse"/></returns>
        Task<FileStationUploadResponse> UploadAsync(string filePath, string destination, bool overwrite);

        /// <summary>
        /// Upload a file 
        /// </summary>
        /// 
        /// <example>
        /// This sample shows how to call the <see cref="UploadAsync"/> method.
        /// <code>
        /// var fileBytes = File.ReadAllBytes(@"C:\myfile.txt");
        /// var uploadResult = await client.FileStationApi().UploadEndpoint().UploadAsync(fileBytes, "myfile.txt", "/my_share/docs", true);
        /// </code>
        /// </example>
        /// 
        /// <param name="bytes">The file contents as a byte array.</param>
        /// <param name="filename">The name used to save the file.</param>
        /// <param name="destination">A destination folder path starting with a shared folder to which files can be uploaded.</param>
        /// <param name="overwrite">Overwrite the destination file if one exists, or skip the upload.</param>
        /// <returns>Returns <see cref="FileStationUploadResponse"/></returns>
        Task<FileStationUploadResponse> UploadAsync(byte[] bytes, string filename, string destination, bool overwrite);
    }
}
