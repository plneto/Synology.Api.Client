using System.Text.Json.Serialization;
using Synology.Api.Client.Constants;

namespace Synology.Api.Client.Apis.Info.Models;

public class InfoQueryResponse
{
    [JsonPropertyName(ApiNames.InfoApiName)]
    public ApiDescription? InfoApi { get; set; }

    [JsonPropertyName(ApiNames.AuthApiName)]
    public ApiDescription? AuthApi { get; set; }

    [JsonPropertyName(ApiNames.FileStationInfoApiName)]
    public ApiDescription? FileStationInfoApi { get; set; }

    [JsonPropertyName(ApiNames.FileStationListApiName)]
    public ApiDescription? FileStationListApi { get; set; }

    [JsonPropertyName(ApiNames.FileStationSearchApiName)]
    public ApiDescription? FileStationSearchApi { get; set; }

    [JsonPropertyName(ApiNames.FileStationVirtualFolderApiName)]
    public ApiDescription? FileStationVirtualFolderApi { get; set; }

    [JsonPropertyName(ApiNames.FileStationFavoriteApiName)]
    public ApiDescription? FileStationFavoriteApi { get; set; }

    [JsonPropertyName(ApiNames.FileStationThumbApiName)]
    public ApiDescription? FileStationThumbApi { get; set; }

    [JsonPropertyName(ApiNames.FileStationDirSizeApiName)]
    public ApiDescription? FileStationDirSizeApi { get; set; }

    [JsonPropertyName(ApiNames.FileStationMd5ApiName)]
    public ApiDescription? FileStationMd5Api { get; set; }

    [JsonPropertyName(ApiNames.FileStationCheckPermissionApiName)]
    public ApiDescription? FileStationCheckPermissionApi { get; set; }

    [JsonPropertyName(ApiNames.FileStationUploadApiName)]
    public ApiDescription? FileStationUploadApi { get; set; }

    [JsonPropertyName(ApiNames.FileStationDownloadApiName)]
    public ApiDescription? FileStationDownloadApi { get; set; }

    [JsonPropertyName(ApiNames.FileStationSharingApiName)]
    public ApiDescription? FileStationSharingApi { get; set; }

    [JsonPropertyName(ApiNames.FileStationCreateFolderApiName)]
    public ApiDescription? FileStationCreateFolderApi { get; set; }

    [JsonPropertyName(ApiNames.FileStationRenameApiName)]
    public ApiDescription? FileStationRenameApi { get; set; }

    [JsonPropertyName(ApiNames.FileStationCopyMoveApiName)]
    public ApiDescription? FileStationCopyMoveApi { get; set; }

    [JsonPropertyName(ApiNames.FileStationDeleteApiName)]
    public ApiDescription? FileStationDeleteApi { get; set; }

    [JsonPropertyName(ApiNames.FileStationExtractApiName)]
    public ApiDescription? FileStationExtractApi { get; set; }

    [JsonPropertyName(ApiNames.FileStationCompressApiName)]
    public ApiDescription? FileStationCompressApi { get; set; }

    [JsonPropertyName(ApiNames.FileStationBackgroundTaskApiName)]
    public ApiDescription? FileStationBackGroundApi { get; set; }

    [JsonPropertyName(ApiNames.DownloadStationInfoApiName)]
    public ApiDescription? DownloadStationInfoApi { get; set; }

    [JsonPropertyName(ApiNames.DownloadStationScheduleApiName)]
    public ApiDescription? DownloadStationScheduleApi { get; set; }

    [JsonPropertyName(ApiNames.DownloadStationTaskApiName)]
    public ApiDescription? DownloadStationTaskApi { get; set; }

    [JsonPropertyName(ApiNames.DownloadStationStatisticApiName)]
    public ApiDescription? DownloadStationStatisticApi { get; set; }

    [JsonPropertyName(ApiNames.DownloadStationRssSiteApiName)]
    public ApiDescription? DownloadStationRssSiteApi { get; set; }

    [JsonPropertyName(ApiNames.DownloadStationRssFeedApiName)]
    public ApiDescription? DownloadStationRssFeedApi { get; set; }
}