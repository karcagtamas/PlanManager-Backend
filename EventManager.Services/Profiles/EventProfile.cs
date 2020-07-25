using System.Collections.Generic;
using AutoMapper;
using ManagerAPI.Domain.Entities.EM;
using ManagerAPI.Domain.Enums.EM;
using ManagerAPI.Shared.DTOs.EM;
using ManagerAPI.Shared.Models.EM;

namespace EventManager.Services.Profiles
{
    public class EventProfile : Profile
    {
        public EventProfile()
        {
            CreateMap<MasterEvent, MyEventListDto>()
                .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator.UserName))
                .ForMember(dest => dest.Members, opt => opt.MapFrom(src => src.Users.Count))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => GetTypeOfEvent(src)));
            CreateMap<MasterEvent, MasterEventDto>()
                .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator.UserName))
                .ForMember(dest => dest.LastUpdater, opt => opt.MapFrom(src => src.LastUpdater.UserName));
            CreateMap<DGtEvent, GtEventDto>();
            CreateMap<DSportEvent, SportEventDto>();
            CreateMap<MasterEvent, EventDto>()
                .ForMember(dest => dest.MasterEvent, opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.GtEvent, opt => opt.MapFrom(src => src.GtEvent))
                .ForMember(dest => dest.SportEvent, opt => opt.MapFrom(src => src.SportEvent));
            CreateMap<EventModel, MasterEvent>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
            CreateMap<MasterEventUpdateDto, MasterEvent>();
            CreateMap<GtEventUpdateDto, DGtEvent>();
            CreateMap<SportEventUpdateDto, DSportEvent>();

            CreateMap<EventAction, EventActionListDto>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User.UserName));
        }

        private string GetTypeOfEvent(MasterEvent masterEvent)
        {
            List<string> types = new List<string>();
            if (masterEvent.GtEvent != null)
            {
                types.Add(EventType.Gt.ToString());
            }

            if (masterEvent.SportEvent != null)
            {
                types.Add(EventType.Sport.ToString());
            }

            return types.Count == 0 ? EventType.Empty.ToString() : string.Join(", ", types);
        }
    }
}