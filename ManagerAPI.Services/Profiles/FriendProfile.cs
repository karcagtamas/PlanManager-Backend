using AutoMapper;
using ManagerAPI.Domain.Entities;
using ManagerAPI.Shared.DTOs;

namespace ManagerAPI.Services.Profiles
{
    /// <summary>
    /// Friend profile for auto mapper
    /// </summary>
    public class FriendProfile : Profile
    {
        public FriendProfile()
        {
            CreateMap<Friends, FriendListDto>()
                .ForMember(dest => dest.Friend, opt => opt.MapFrom(src => src.Friend.UserName))
                .ForMember(dest => dest.FriendId, opt => opt.MapFrom(src => src.Friend.Id))
                .ForMember(dest => dest.FriendFullName, opt => opt.MapFrom(src => src.Friend.FullName))
                .ForMember(dest => dest.FriendImageTitle, opt => opt.MapFrom(src => src.Friend.ProfileImageTitle))
                .ForMember(dest => dest.FriendImageData, opt => opt.MapFrom(src => src.Friend.ProfileImageData));

            CreateMap<FriendRequest, FriendRequestListDto>()
                .ForMember(dest => dest.Sender, opt => opt.MapFrom(src => src.Sender.UserName))
                .ForMember(dest => dest.SenderFullName, opt => opt.MapFrom(src => src.Sender.FullName));

            CreateMap<User, FriendDataDto>()
                .ForMember(dest => dest.Roles, opt => opt.Ignore())
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender.Name));
        }
    }
}
