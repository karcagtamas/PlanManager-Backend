using System.Threading.Tasks;
using EventManager.Client.Models;

namespace EventManager.Client.Services
{
    public interface IAuthService
    {
        Task<ApiResponseModel<object>> Register(RegistrationModel model);
        Task<ApiResponseModel<string>> Login(LoginModel model);
        Task Logout();
    }
}