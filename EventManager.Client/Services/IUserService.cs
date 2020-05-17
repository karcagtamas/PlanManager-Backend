using System.Collections.Generic;
using System.Threading.Tasks;
using EventManager.Client.Models;
using EventManager.Client.Models.User;

namespace EventManager.Client.Services
{
    public interface IUserService
    {
        Task<ApiResponseModel<UserDto>> GetUser();

        Task<ApiResponseModel<object>> UpdateUser(UserUpdateDto userUpdate);

        Task<ApiResponseModel<List<GenderDto>>> GetGenders();
    }
}