using Synology.Api.Client.Apis.FileStation.CopyMove;
using Synology.Api.Client.Apis.FileStation.Extract;
using Synology.Api.Client.Apis.FileStation.List;
using Synology.Api.Client.Apis.FileStation.Upload;

namespace Synology.Api.Client.Apis.FileStation
{
    public interface IFileStationApi
    {
        IFileStationCopyMoveEndpoint CopyMoveEndpoint();

        IFileStationListEndpoint ListEndpoint();

        IFileStationUploadEndpoint UploadEndpoint();

        IFileStationExtractEndpoint ExtractEndpoint();
    }
}
