﻿using AutoMapper;
using ManagerAPI.Domain.Entities;
using ManagerAPI.Shared.DTOs;

namespace ManagerAPI.Services.Profiles
{
    /// <summary>
    /// Notification profile for auto mapper
    /// </summary>
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