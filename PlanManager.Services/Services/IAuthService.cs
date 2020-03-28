using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using PlanManager.DataAccess.Models;

namespace PlanManager.Services.Services
{
    public interface IAuthService
    {
        Task<IdentityResult> Registration(RegistrationModel model);
        Task<string> Login(LoginModel model);
    }
}