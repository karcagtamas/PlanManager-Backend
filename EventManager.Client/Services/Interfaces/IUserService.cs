using System.Threading.Tasks;
using ManagerAPI.Shared.DTOs;
using ManagerAPI.Shared.Models;

namespace EventManager.Client.Services.Interfaces {
    public interface IUserService {
        Task<UserDto> GetUser ();
        Task<UserShortDto> GetShortUser ();
        Task<bool> UpdateUser (UserModel userUpdate);
        Task<bool> UpdatePassword (PasswordUpdateModel model);
        Task<bool> UpdateProfileImage (byte[] image);
        Task<bool> UpdateUsername (UsernameUpdateModel model);
        Task<bool> DisableUser ();
    }
}