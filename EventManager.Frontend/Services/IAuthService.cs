using System.Threading.Tasks;
using EventManager.Frontend.Data.Models;

namespace EventManager.Frontend.Services
{
    public interface IAuthService
    {
        Task<ApiResponse<string>> SignIn(LoginModel model);
    }
}