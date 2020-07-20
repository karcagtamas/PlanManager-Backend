using System.Threading.Tasks;
using ManagerAPI.Shared.Models;

namespace ManagerAPI.Services.Services.Interfaces
{
    public interface IAuthService
    {
        Task Registration(RegistrationModel model);
        Task<string> Login(LoginModel model);
        void Logout(string userId);
    }
}