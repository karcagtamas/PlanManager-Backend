using AutoMapper;
using ManagerAPI.Models.DTOs;
using ManagerAPI.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManagerAPI.Services.Profiles
{
    public class MessageProfile : Profile
    {
        public MessageProfile()
        {
            CreateMap<Message, MessageDto>()
                .ForMember(dest => dest.IsMine, opt => opt.Ignore())
                .ForMember(dest => dest.Sender, opt => opt.MapFrom(x => x.Sender.UserName));
        }
    }
}
