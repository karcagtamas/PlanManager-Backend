using System.Collections.Generic;
using System.Threading.Tasks;
using ManagerAPI.Models.DTOs;
using Microsoft.AspNetCore.Identity;

namespace ManagerAPI.Services.Services
{
    public interface IUserService
    {
        Task<UserDto> GetUser();
        UserShortDto GetShortUser();
        void UpdateUser(UserUpdateDto updateDto);
        List<GenderDto> GetGenders();
        void UpdateProfileImage(byte[] image);
        Task UpdatePassword(string oldPassword, string newPassword);
        Task UpdateUsername(string newUsername);
        void DisableUser();
    }
}