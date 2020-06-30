using System.Threading.Tasks;
using EventManager.Client.Models;

namespace EventManager.Client.Services {
    public interface IAuthService {
        Task<bool> Register (RegistrationModel model);
        Task<string> Login (LoginModel model);
        Task Logout ();
    }
}