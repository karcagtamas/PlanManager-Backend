using System;
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
                .ForMember(dest => dest.HourAvg, opt => opt.MapFrom(src => this.CalcAvgHour(src)))
                .ForMember(dest => dest.ActiveDays, opt => opt.MapFrom(src => this.ActiveDays(src)))
                .ForMember(dest => dest.WorkDays, opt => opt.MapFrom(src => this.WorkDays(src)))
                .ForMember(dest => dest.Counts, opt => opt.MapFrom(src => this.CalcCounts(src)));
            CreateMap<List<WorkingField>, WorkingWeekStatDto>()
                .ForMember(dest => dest.Fields, opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.HourSum, opt => opt.MapFrom(src => this.CalcSumHour(src)))
                .ForMember(dest => dest.HourAvg, opt => opt.MapFrom(src => this.CalcAvgHour(src)))
                .ForMember(dest => dest.ActiveDays, opt => opt.MapFrom(src => this.ActiveDays(src)))
                .ForMember(dest => dest.WorkDays, opt => opt.MapFrom(src => this.WorkDays(src)))
                .ForMember(dest => dest.Counts, opt => opt.MapFrom(src => this.CalcCounts(src)));
        }

        private decimal CalcSumHour(List<WorkingField> fields)
        {
            return fields == null || !fields.Any() ? 0 : fields.Where(x => x.WorkingDay.Type.DayIsActive).Sum(x => x.Length);
        }

        private double CalcAvgHour(List<WorkingField> fields)
        {
            return fields == null || !fields.Any() ? 0 : (double)CalcSumHour(fields) / this.ActiveDays(fields);
        }

        private int ActiveDays(List<WorkingField> fields) 
        {
            return fields.GroupBy(x => x.WorkingDay).Select(x => x.Key).Count(x => x.Type.DayIsActive);
        }

        private int WorkDays(List<WorkingField> fields)
        {
            if (!fields.Any())
            {
                return 0;
            }

            int count = 0;
            int year = fields[0].WorkingDay.Day.Year;
            int month = fields[0].WorkingDay.Day.Month;
            var date = new DateTime(year, month, 1);
            var monthDays = DateTime.DaysInMonth(year, month);
            
            while (date.Day != monthDays)
            {
                if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
                {
                    count++;
                }

                date = date.AddDays(1);
            }

            return count;
        }

        private List<WorkingDayTypeCountDto> CalcCounts(List<WorkingField> fields) 
        {
            return fields.GroupBy(x => x.WorkingDay.Type).Select(x => new WorkingDayTypeCountDto { Type = x.Key.Title, Count = x.GroupBy(y => y.WorkingDay).Count() }).ToList();
        }
    }
}