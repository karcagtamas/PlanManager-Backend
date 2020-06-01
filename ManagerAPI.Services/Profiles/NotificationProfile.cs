using AutoMapper;
using ManagerAPI.Models.DTOs;
using ManagerAPI.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManagerAPI.Services.Profiles
{
    public class NotificationProfile : Profile
    {
        public NotificationProfile()
        {
            CreateMap<Notification, NotificationDto>()
                .ForMember(dest => dest.ImportanceLevel, opt => opt.MapFrom(src => src.Type.ImportanceLevel))
                .ForMember(dest => dest.TypeTitle, opt => opt.MapFrom(src => src.Type.Title))
                .ForMember(dest => dest.SystemName, opt => opt.MapFrom(src => src.Type.System.Name))
                .ForMember(dest => dest.SystemShortName, opt => opt.MapFrom(src => src.Type.System.ShortName));
        }
    }
}
