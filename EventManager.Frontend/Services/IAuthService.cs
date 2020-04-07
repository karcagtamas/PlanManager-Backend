using System.Threading.Tasks;

namespace EventManager.Frontend.Services
{
    public interface IAuthService
    {
        Task Login(string userName, string password);
    }
}