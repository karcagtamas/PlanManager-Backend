using AutoMapper;
using ManagerAPI.Domain.Entities.WM;
using ManagerAPI.Shared.DTOs.WM;

namespace ManagerAPI.Services.Profiles
{
    public class WorkingDayTypeProfile : Profile
    {
        public WorkingDayTypeProfile()
        {
            CreateMap<WorkingDayType, WorkingDayTypeDto>();
            CreateMap<WorkingDayType, WorkingDayTypeListDto>();
            CreateMap<WorkingDayTypeModel, WorkingDayType>()
                .ForMember(dest => dest.WorkingDays, opt => opt.Ignore());
        }
    }
}