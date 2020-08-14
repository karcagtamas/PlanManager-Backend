using AutoMapper;
using ManagerAPI.Domain.Entities;
using ManagerAPI.Shared.DTOs;
using ManagerAPI.Shared.Models;

namespace ManagerAPI.Services.Profiles {
    public class UserProfile : Profile {
        public UserProfile ()
        {
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Roles, opt => opt.Ignore())
                .ForMember(dest => dest.GenderId, opt => opt.MapFrom(src => src.Gender == null ? null : (int?)src.Gender.Id));

            CreateMap<UserModel, User>();
            CreateMap<User, UserShortDto>();
        }
    }
}