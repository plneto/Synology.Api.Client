namespace Synology.Api.Client.Apis.DownloadStation.Task.Models
{
    public class DownloadStationTaskCreateRequest
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="uri">Optional. Accepts HTTP/FTP/magnet/ED2K links or the file
        /// path starting with a shared folder, separated by ","</param>
        /// <param name="destination">Optional. Download destination path starting with a shared folder</param>
        public DownloadStationTaskCreateRequest(string uri, string destination = null)
        {
            Uri = uri;
            Destination = destination;
        }
        
        /// <summary>
        /// Optional. Accepts HTTP/FTP/magnet/ED2K links or the file
        /// path starting with a shared folder, separated by ","
        /// </summary>
        /// <returns></returns>
        public string Uri { get; }

        /// <summary>
        /// Optional. Download destination path starting with a shared folder
        /// </summary>
        public string Destination { get; }
    }
}