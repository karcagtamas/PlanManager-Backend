using AutoMapper;
using ManagerAPI.Models.DTOs.WM;
using ManagerAPI.Models.Entities.WM;
using ManagerAPI.Models.Models.WM;

namespace WorkingManager.Services.Profiles
{
    public class WorkingManagerProfile : Profile
    {
        public WorkingManagerProfile()
        {
            CreateMap<WorkingDay, WorkingDayListDto>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.Id));
            CreateMap<WorkingField, WorkingFieldListDto>();
            CreateMap<WorkingDayModel, WorkingDay>()
                .ForMember(dest => dest.TypeId, opt => opt.MapFrom(src => src.Type))
                .ForMember(dest => dest.Type, opt => opt.Ignore());
            CreateMap<WokringFieldModel, WorkingField>()
                .ForMember(dest => dest.WorkingDayId, opt => opt.Ignore())
                .ForMember(dest => dest.WorkingDay, opt => opt.Ignore());
            CreateMap<WorkingDayType, WorkingDayTypeDto>();
        }
    }
}
