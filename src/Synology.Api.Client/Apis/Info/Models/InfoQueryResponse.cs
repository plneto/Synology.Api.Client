using Newtonsoft.Json;
using Synology.Api.Client.Constants;

namespace Synology.Api.Client.Apis.Info.Models
{
    public class InfoQueryResponse
    {
        [JsonProperty(ApiNames.InfoApiName)]
        public ApiDescription InfoApi { get; set; }

        [JsonProperty(ApiNames.AuthApiName)]
        public ApiDescription AuthApi { get; set; }

        [JsonProperty(ApiNames.FileStationInfoApiName)]
        public ApiDescription FileStationInfoApi { get; set; }

        [JsonProperty(ApiNames.FileStationListApiName)]
        public ApiDescription FileStationListApi { get; set; }

        [JsonProperty(ApiNames.FileStationSearchApiName)]
        public ApiDescription FileStationSearchApi { get; set; }

        [JsonProperty(ApiNames.FileStationVirtualFolderApiName)]
        public ApiDescription FileStationVirtualFolderApi { get; set; }

        [JsonProperty(ApiNames.FileStationFavoriteApiName)]
        public ApiDescription FileStationFavoriteApi { get; set; }

        [JsonProperty(ApiNames.FileStationThumbApiName)]
        public ApiDescription FileStationThumbApi { get; set; }

        [JsonProperty(ApiNames.FileStationDirSizeApiName)]
        public ApiDescription FileStationDirSizeApi { get; set; }

        [JsonProperty(ApiNames.FileStationMd5ApiName)]
        public ApiDescription FileStationMd5Api { get; set; }

        [JsonProperty(ApiNames.FileStationCheckPermissionApiName)]
        public ApiDescription FileStationCheckPermissionApi { get; set; }

        [JsonProperty(ApiNames.FileStationUploadApiName)]
        public ApiDescription FileStationUploadApi { get; set; }

        [JsonProperty(ApiNames.FileStationDownloadApiName)]
        public ApiDescription FileStationDownloadApi { get; set; }

        [JsonProperty(ApiNames.FileStationSharingApiName)]
        public ApiDescription FileStationSharingApi { get; set; }

        [JsonProperty(ApiNames.FileStationCreateFolderApiName)]
        public ApiDescription FileStationCreateFolderApi { get; set; }

        [JsonProperty(ApiNames.FileStationRenameApiName)]
        public ApiDescription FileStationRenameApi { get; set; }

        [JsonProperty(ApiNames.FileStationCopyMoveApiName)]
        public ApiDescription FileStationCopyMoveApi { get; set; }

        [JsonProperty(ApiNames.FileStationDeleteApiName)]
        public ApiDescription FileStationDeleteApi { get; set; }

        [JsonProperty(ApiNames.FileStationExtractApiName)]
        public ApiDescription FileStationExtractApi { get; set; }

        [JsonProperty(ApiNames.FileStationCompressApiName)]
        public ApiDescription FileStationCompressApi { get; set; }

        [JsonProperty(ApiNames.FileStationBackgroundTaskApiName)]
        public ApiDescription FileStationBackGroundApi { get; set; }

        [JsonProperty(ApiNames.DownloadStationInfoApiName)]
        public ApiDescription DownloadStationInfoApi { get; set; }

        [JsonProperty(ApiNames.DownloadStationScheduleApiName)]
        public ApiDescription DownloadStationScheduleApi { get; set; }

        [JsonProperty(ApiNames.DownloadStationTaskApiName)]
        public ApiDescription DownloadStationTaskApi { get; set; }

        [JsonProperty(ApiNames.DownloadStationStatisticApiName)]
        public ApiDescription DownloadStationStatisticApi { get; set; }

        [JsonProperty(ApiNames.DownloadStationRssSiteApiName)]
        public ApiDescription DownloadStationRssSiteApi { get; set; }

        [JsonProperty(ApiNames.DownloadStationRssFeedApiName)]
        public ApiDescription DownloadStationRssFeedApi { get; set; }
    }
}
