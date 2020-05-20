using System.Collections.Generic;
using System.Threading.Tasks;
using EventManager.Client.Models;
using EventManager.Client.Models.User;
using PasswordUpdateModel = EventManager.Client.Models.PasswordUpdateModel;

namespace EventManager.Client.Services
{
    public interface IUserService
    {
        Task<ApiResponseModel<UserDto>> GetUser();

        Task<ApiResponseModel<object>> UpdateUser(UserUpdateDto userUpdate);

        Task<ApiResponseModel<List<GenderDto>>> GetGenders();

        Task<ApiResponseModel<object>> UpdatePassword(PasswordUpdateModel model);

        Task<ApiResponseModel<object>> UpdateProfileImage(byte[] image);
        Task<ApiResponseModel<object>> UpdateUsername(UsernameUpdateModel model);
        Task<ApiResponseModel<object>> DisableUser();
    }
}