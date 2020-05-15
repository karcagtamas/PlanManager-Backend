using System.Threading.Tasks;
using ManagerAPI.DataAccess.Models;
using Microsoft.AspNetCore.Identity;

namespace ManagerAPI.Services.Services
{
    public interface IAuthService
    {
        Task<IdentityResult> Registration(RegistrationModel model);
        Task<string> Login(LoginModel model);
    }
}