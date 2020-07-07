using System.Threading.Tasks;
using ManagerAPI.Models.Models;
using Microsoft.AspNetCore.Identity;

namespace ManagerAPI.Services.Services.Interfaces
{
    public interface IAuthService
    {
        Task Registration(RegistrationModel model);
        Task<string> Login(LoginModel model);
        void Logout(string userId);
    }
}