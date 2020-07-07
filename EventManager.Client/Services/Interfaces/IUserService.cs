using System.Collections.Generic;
using System.Threading.Tasks;
using EventManager.Client.Models;
using EventManager.Client.Models.User;

namespace EventManager.Client.Services.Interfaces {
    public interface IUserService {
        Task<UserDto> GetUser ();
        Task<UserShortDto> GetShortUser ();
        Task<bool> UpdateUser (UserUpdateDto userUpdate);
        Task<List<GenderDto>> GetGenders ();
        Task<bool> UpdatePassword (PasswordUpdateModel model);
        Task<bool> UpdateProfileImage (byte[] image);
        Task<bool> UpdateUsername (UsernameUpdateModel model);
        Task<bool> DisableUser ();
    }
}