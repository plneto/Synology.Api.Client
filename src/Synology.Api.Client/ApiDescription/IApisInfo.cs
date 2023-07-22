namespace Synology.Api.Client.ApiDescription
{
    public interface IApisInfo
    {
        IApiInfo InfoApi { get; set; }

        IApiInfo AuthApi { get; set; }

        IApiInfo DownloadStationTaskApi { get; set; }
        
        IApiInfo DownloadStationInfoApi { get; set; }

        IApiInfo FileStationCopyMoveApi { get; set; }

        IApiInfo FileStationCreateFolderApi { get; set; }

        IApiInfo FileStationExtractApi { get; set; }

        IApiInfo FileStationListApi { get; set; }

        IApiInfo FileStationUploadApi { get; set; }
        
        IApiInfo FileStationSearchApi { get; set; }
    }
}
