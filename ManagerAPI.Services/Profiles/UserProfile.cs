using AutoMapper;
using ManagerAPI.DataAccess.Entities;
using ManagerAPI.Services.DTOs;

namespace ManagerAPI.Services.Profiles {
    public class UserProfile : Profile {
        public UserProfile ()
        {
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Roles, opt => opt.Ignore());
        }
    }
}