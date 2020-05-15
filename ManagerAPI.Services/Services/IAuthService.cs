using System.Threading.Tasks;
using ManagerAPI.Models.Models;
using Microsoft.AspNetCore.Identity;

namespace ManagerAPI.Services.Services
{
    public interface IAuthService
    {
        Task<IdentityResult> Registration(RegistrationModel model);
        Task<string> Login(LoginModel model);
    }
}