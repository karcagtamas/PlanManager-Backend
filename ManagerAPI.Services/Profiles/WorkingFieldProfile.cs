using System.Collections.Generic;
using System.Linq;
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
            CreateMap<List<WorkingField>, WorkingMonthStatDto>()
                .ForMember(dest => dest.Fields, opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.HourSum, opt => opt.MapFrom(src => this.CalcSumHour(src)))
                .ForMember(dest => dest.HourAvg, opt => opt.MapFrom(src => this.CalcAvgHour(src)));
            CreateMap<List<WorkingField>, WorkingWeekStatDto>()
                .ForMember(dest => dest.Fields, opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.HourSum, opt => opt.MapFrom(src => this.CalcSumHour(src)))
                .ForMember(dest => dest.HourAvg, opt => opt.MapFrom(src => this.CalcAvgHour(src)));
        }

        private decimal CalcSumHour(List<WorkingField> fields)
        {
            return fields == null || !fields.Any() ? 0 : fields.Where(x => x.WorkingDay.Type.DayIsActive).Sum(x => x.Length);
        }

        private double CalcAvgHour(List<WorkingField> fields)
        {
            return fields == null || !fields.Any() ? 0 : (double)CalcSumHour(fields) / fields.GroupBy(x => x.WorkingDay).Select(x => x.Key).Count(x => x.Type.DayIsActive);
        }
    }
}