using System.Threading.Tasks;
using PlanManager.Services.DTOs;

namespace PlanManager.Services.Services
{
    public interface IUserService
    {
        Task<UserDto> GetUser();
        void UpdateUser(UserUpdateDto updateDto);
    }
}