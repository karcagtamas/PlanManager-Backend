using System.Threading.Tasks;
using EventManager.Client.Models.Auth;

namespace EventManager.Client.Services.Interfaces {
    public interface IAuthService {
        Task<bool> Register (RegistrationModel model);
        Task<string> Login (LoginModel model);
        Task Logout ();
    }
}