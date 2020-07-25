using AutoMapper;
using ManagerAPI.Domain.Entities.WM;
using ManagerAPI.Shared.DTOs.WM;
using ManagerAPI.Shared.Models.WM;

namespace ManagerAPI.Services.Profiles
{
    public class WorkingFieldProfile : Profile
    {
        public WorkingFieldProfile()
        {
            CreateMap<WorkingField, WorkingFieldListDto>();
            CreateMap<WorkingField, WorkingFieldDto>();
            CreateMap<WorkingFieldModel, WorkingField>()
                .ForMember(dest => dest.WorkingDayId, opt => opt.MapFrom(src => src.WorkingDayId))
                .ForMember(dest => dest.WorkingDay, opt => opt.Ignore());
        }
    }
}