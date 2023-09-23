using System.Threading.Tasks;
using Synology.Api.Client.Apis.Auth.Models;
using Synology.Api.Client.Shared.Models;

namespace Synology.Api.Client.Apis.Auth
{
    public interface IAuthApi
    {
        Task<LoginResponse> LoginAsync(string username, string password, string? otpCode = null);

        Task<BaseApiResponse> LogoutAsync(string sid);
    }
}
