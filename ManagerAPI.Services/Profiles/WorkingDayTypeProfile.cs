using AutoMapper;
using ManagerAPI.Domain.Entities.WM;
using ManagerAPI.Shared.DTOs.WM;
using ManagerAPI.Shared.Models.WM;

namespace ManagerAPI.Services.Profiles
{
    /// <summary>
    /// Working Day Type profile for auto mapper
    /// </summary>
    public class WorkingDayTypeProfile : Profile
    {
        public WorkingDayTypeProfile()
        {
            this.CreateMap<WorkingDayType, WorkingDayTypeDto>();
            this.CreateMap<WorkingDayType, WorkingDayTypeListDto>();
            this.CreateMap<WorkingDayTypeModel, WorkingDayType>()
                .ForMember(dest => dest.WorkingDays, opt => opt.Ignore());
        }
    }
}