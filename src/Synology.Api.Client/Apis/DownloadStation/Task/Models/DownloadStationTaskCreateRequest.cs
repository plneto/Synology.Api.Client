using Newtonsoft.Json;

namespace Synology.Api.Client.Apis.DownloadStation.Task.Models
{
    public class DownloadStationTaskCreateRequest
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="uri">Optional. Accepts HTTP/FTP/magnet/ED2K links or the file
        /// path starting with a shared folder, separated by ","</param>
        /// <param name="file">Optional. File uploading from client</param>
        /// <param name="username">Optional. Login username</param>
        /// <param name="password">Optional. Login password</param>
        /// <param name="unzipPassword">Optional. Password for unzipping download tasks</param>
        /// <param name="destination">Optional. Download destination path starting with a shared folder</param>
        public DownloadStationTaskCreateRequest(string uri = null, string file = null, string username = null, 
            string password = null, string unzipPassword = null, string destination = null)
        {
            Uri = uri;
            File = file;
            Username = username;
            Password = password;
            UnzipPassword = unzipPassword;
            Destination = destination;
        }
        
        /// <summary>
        /// Optional. Accepts HTTP/FTP/magnet/ED2K links or the file
        /// path starting with a shared folder, separated by ","
        /// </summary>
        /// <returns></returns>
        public string Uri { get; }
        
        /// <summary>
        /// Optional. File uploading from client
        /// </summary>
        public string File { get; }
        
        /// <summary>
        /// Optional. Login username
        /// </summary>
        public string Username { get; }
        
        /// <summary>
        /// Optional. Login password
        /// </summary>
        public string Password { get; }

        /// <summary>
        /// Optional. Password for unzipping download tasks 
        /// </summary>
        public string UnzipPassword { get; }
        
        /// <summary>
        /// Optional. Download destination path starting with a shared folder
        /// </summary>
        public string Destination { get; }
    }
}