using System.Collections.Generic;
using System.Threading.Tasks;
using ManagerAPI.Shared.Models;

namespace EventManager.Client.Services.Interfaces {
    public interface IAuthService {
        Task<bool> Register (RegistrationModel model);
        Task<string> Login (LoginModel model);
        Task Logout ();
        Task<bool> HasRole(params string[] roles);
        public Task<bool> IsLoggedIn();
    }
}