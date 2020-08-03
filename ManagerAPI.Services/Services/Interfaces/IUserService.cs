using System.Collections.Generic;
using System.Threading.Tasks;
using ManagerAPI.Shared.DTOs;

namespace ManagerAPI.Services.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> GetUser();
        UserShortDto GetShortUser();
        void UpdateUser(UserUpdateDto updateDto);
        void UpdateProfileImage(byte[] image);
        Task UpdatePassword(string oldPassword, string newPassword);
        Task UpdateUsername(string newUsername);
        void DisableUser();
    }
}