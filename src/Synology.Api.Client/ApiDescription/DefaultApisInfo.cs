using Synology.Api.Client.ApiDescription;
using Synology.Api.Client.Constants;

namespace Synology.Api.Client.Apis
{
    public class DefaultApisInfo : IApisInfo
    {
        private const string FileStationSessionName = "FileStation";
        private const string DownloadStationSessionName = "DownloadStation";

        public DefaultApisInfo()
        {
            InfoApi = new ApiInfo(
                ApiNames.InfoApiName,
                "query.cgi",
                1);

            AuthApi = new ApiInfo(
                ApiNames.AuthApiName,
                "auth.cgi",
                3,
                FileStationSessionName);

            DownloadStationTaskApi = new ApiInfo(
                ApiNames.DownloadStationTaskApiName,
                "DownloadStation/task.cgi",
                1,
                DownloadStationSessionName);

            FileStationCopyMoveApi = new ApiInfo(
                ApiNames.FileStationCopyMoveApiName,
                "entry.cgi",
                3,
                FileStationSessionName);

            FileStationExtractApi = new ApiInfo(
                ApiNames.FileStationExtractApiName,
                "entry.cgi",
                2,
                FileStationSessionName);

            FileStationListApi = new ApiInfo(
                ApiNames.FileStationListApiName,
                "entry.cgi",
                2,
                FileStationSessionName);

            FileStationUploadApi = new ApiInfo(
                ApiNames.FileStationUploadApiName,
                "entry.cgi",
                2,
                FileStationSessionName);
        }

        public IApiInfo InfoApi { get; set; }

        public IApiInfo AuthApi { get; set; }

        public IApiInfo DownloadStationTaskApi { get; set; }

        public IApiInfo FileStationCopyMoveApi { get; set; }

        public IApiInfo FileStationExtractApi { get; set; }

        public IApiInfo FileStationListApi { get; set; }

        public IApiInfo FileStationUploadApi { get; set; }
    }
}
