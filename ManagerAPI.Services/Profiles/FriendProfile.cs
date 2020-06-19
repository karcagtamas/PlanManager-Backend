using AutoMapper;
using ManagerAPI.Models.DTOs;
using ManagerAPI.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManagerAPI.Services.Profiles
{
    public class FriendProfile : Profile
    {
        public FriendProfile()
        {
            CreateMap<Friends, FriendListDto>()
                .ForMember(dest => dest.Friend, opt => opt.MapFrom(src => src.Friend.UserName));

            CreateMap<FriendRequest, FriendRequestListDto>()
                .ForMember(dest => dest.Sender, opt => opt.MapFrom(src => src.Sender.UserName));
        }
    }
}
