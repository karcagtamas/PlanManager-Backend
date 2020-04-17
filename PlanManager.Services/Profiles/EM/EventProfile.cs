using System.Collections.Generic;
using AutoMapper;
using PlanManager.DataAccess.Entities.EM;
using PlanManager.Services.DTOs.EM;

namespace PlanManager.Services.Profiles.EM
{
    public class EventProfile : Profile
    {
        public EventProfile()
        {
            CreateMap<MasterEvent, MyEventListDto>()
                .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator.UserName))
                .ForMember(dest => dest.Members, opt => opt.MapFrom(src => src.Users.Count))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.GtEvent != null ? "GT" : src.SportEvent != null ? "Sport" : "Empty"));
                    CreateMap<List<MasterEvent>, List<MyEventListDto>>();
        }
    }
}