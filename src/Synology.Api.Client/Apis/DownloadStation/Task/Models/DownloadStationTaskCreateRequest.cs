using Newtonsoft.Json;

namespace Synology.Api.Client.Apis.DownloadStation.Task.Models
{
    public class DownloadStationTaskCreateRequest
    {
        /// <summary>
        /// Optional. Accepts HTTP/FTP/magnet/ED2K links or the file
        /// path starting with a shared folder, separated by ",".
        /// </summary>
        public string Uri { get; set; }
        
        /// <summary>
        /// Optional. File uploading from client
        /// </summary>
        public string File { get; set; }
        
        /// <summary>
        /// Optional. Login username
        /// </summary>
        public string Username { get; set; }
        
        /// <summary>
        /// Optional. Login password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Optional. Password for unzipping download tasks 
        /// </summary>
        [JsonProperty("unzip_password")]
        public string UnzipPassword { get; set; }
        
        /// <summary>
        /// Optional. Download destination path starting with a shared folder
        /// </summary>
        public string Destination { get; set; }
    }
}