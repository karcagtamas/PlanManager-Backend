using AutoMapper;
using PlanManager.DataAccess.Entities;
using PlanManager.Services.DTOs;

namespace PlanManager.Services.Profiles {
    public class UserProfile : Profile {
        public UserProfile ()
        {
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Roles, opt => opt.Ignore());
        }
    }
}