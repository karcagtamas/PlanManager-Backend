using AutoMapper;
using ManagerAPI.Models.DTOs;
using ManagerAPI.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManagerAPI.Services.Profiles
{
    public class NewsProfile : Profile
    {
        public NewsProfile()
        {
            CreateMap<News, NewsDto>()
                .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator.UserName))
                .ForMember(dest => dest.LastUpdater, opt => opt.MapFrom(src => src.LastUpdater.UserName));
        }
    }
}
