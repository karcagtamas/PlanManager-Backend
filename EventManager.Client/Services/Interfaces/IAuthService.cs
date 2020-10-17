using ManagerAPI.Shared.Models;
using System.Threading.Tasks;

namespace EventManager.Client.Services.Interfaces
{
    public interface IAuthService
    {
        Task<bool> Register(RegistrationModel model);
        Task<string> Login(LoginModel model);
        Task Logout();
        Task<bool> HasRole(params string[] roles);
        public Task<bool> IsLoggedIn();
    }
}