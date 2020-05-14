using System.Threading.Tasks;
using ManagerAPI.Services.DTOs;

namespace ManagerAPI.Services.Services
{
    public interface IUserService
    {
        Task<UserDto> GetUser();
        void UpdateUser(UserUpdateDto updateDto);
    }
}