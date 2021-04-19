using System.Threading.Tasks;
using Synology.Api.Client.Apis.Info.Models;

namespace Synology.Api.Client.Apis.Info
{
    public interface IInfoEndpoint
    {
        Task<InfoQueryResponse> QueryAsync();
    }
}
