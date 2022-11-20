using Synology.Api.Client.ApiDescription;
using Synology.Api.Client.Apis.FileStation.CopyMove;
using Synology.Api.Client.Apis.FileStation.CreateFolder;
using Synology.Api.Client.Apis.FileStation.Extract;
using Synology.Api.Client.Apis.FileStation.List;
using Synology.Api.Client.Apis.FileStation.Search;
using Synology.Api.Client.Apis.FileStation.Upload;
using Synology.Api.Client.Session;

namespace Synology.Api.Client.Apis.FileStation
{
    public class FileStationApi : IFileStationApi
    {
        private readonly ISynologyHttpClient _synologyHttpClient;
        private readonly IApisInfo _apisInfo;
        private readonly ISynologySession _session;

        public FileStationApi(ISynologyHttpClient synologyHttpClient, IApisInfo apisInfo, ISynologySession session)
        {
            _synologyHttpClient = synologyHttpClient;
            _apisInfo = apisInfo;
            _session = session;
        }

        public IFileStationCopyMoveEndpoint CopyMoveEndpoint()
        {
            return new FileStationCopyMoveEndpoint(_synologyHttpClient, _apisInfo.FileStationCopyMoveApi, _session);
        }

        public IFileStationCreateFolderEndpoint CreateFolderEndpoint()
        {
            return new FileStationCreateFolderEndpoint(_synologyHttpClient, _apisInfo.FileStationCreateFolderApi, _session);
        }

        public IFileStationListEndpoint ListEndpoint()
        {
            return new FileStationListEndpoint(_synologyHttpClient, _apisInfo.FileStationListApi, _session);
        }

        public IFileStationUploadEndpoint UploadEndpoint()
        {
            return new FileStationUploadEndpoint(_synologyHttpClient, _apisInfo.FileStationUploadApi, _session);
        }

        public IFileStationExtractEndpoint ExtractEndpoint()
        {
            return new FileStationExtractEndpoint(_synologyHttpClient, _apisInfo.FileStationExtractApi, _session);
        }

        public IFileStationSearchEndpoint SearchEndpoint()
        {
            return new FileStationSearchEndpoint(_synologyHttpClient, _apisInfo.FileStationSearchApi, _session);
        }
    }
}
