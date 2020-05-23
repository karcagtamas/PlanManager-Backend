using AutoMapper;
using ManagerAPI.Models.DTOs;
using ManagerAPI.Models.Entities;

namespace WorkingManager.Services.Profiles
{
    public class WorkingManagerProfile : Profile
    {
        public WorkingManagerProfile()
        {
            CreateMap<WorkingDay, WorkingDayListDto>()
                .ForMember(dest => dest.WorkingFields, opt => opt.Ignore());
            CreateMap<WorkingField, WorkingFieldListDto>();
            CreateMap<WorkingDayDto, WorkingDay>()
                .ForMember(dest => dest.TypeId, opt => opt.MapFrom(src => src.Type))
                .ForMember(dest => dest.Type, opt => opt.Ignore());
            CreateMap<WorkingFieldDto, WorkingField>();
            CreateMap<WorkingDayType, WorkingDayTypeDto>();
        }
    }
}
