using AutoMapper;
using ManagerAPI.Models.DTOs;
using ManagerAPI.Models.Entities;

namespace ManagerAPI.Services.Profiles {
    public class UserProfile : Profile {
        public UserProfile ()
        {
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Roles, opt => opt.Ignore())
                .ForMember(dest => dest.GenderId, opt => opt.MapFrom(src => src.Gender == null ? null : (int?)src.Gender.Id));

            CreateMap<UserUpdateDto, User>();
            CreateMap<Gender, GenderDto>();
        }
    }
}