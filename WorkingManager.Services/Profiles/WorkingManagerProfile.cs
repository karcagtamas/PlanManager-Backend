using AutoMapper;
using ManagerAPI.Models.DTOs.WM;
using ManagerAPI.Models.Entities.WM;

namespace WorkingManager.Services.Profiles
{
    public class WorkingManagerProfile : Profile
    {
        public WorkingManagerProfile()
        {
            CreateMap<WorkingDay, WorkingDayListDto>();
            CreateMap<WorkingField, WorkingFieldListDto>();
            CreateMap<WorkingDayDto, WorkingDay>()
                .ForMember(dest => dest.TypeId, opt => opt.MapFrom(src => src.Type))
                .ForMember(dest => dest.Type, opt => opt.Ignore());
            CreateMap<WorkingFieldDto, WorkingField>()
                .ForMember(dest => dest.WorkingDayId, opt => opt.Ignore())
                .ForMember(dest => dest.WorkingDay, opt => opt.Ignore());
            CreateMap<WorkingDayType, WorkingDayTypeDto>();
        }
    }
}
