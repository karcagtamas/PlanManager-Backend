using System.Collections.Generic;
using System.Threading.Tasks;
using ManagerAPI.Models.DTOs;

namespace ManagerAPI.Services.Services
{
    public interface IUserService
    {
        Task<UserDto> GetUser();
        void UpdateUser(UserUpdateDto updateDto);
        List<GenderDto> GetGenders();
    }
}