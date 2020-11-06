using AutoMapper;
using ManagerAPI.Domain.Entities.WM;
using ManagerAPI.Shared.DTOs.WM;
using ManagerAPI.Shared.Models.WM;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ManagerAPI.Services.Profiles
{
    /// <summary>
    /// Working Field profile for auto mapper
    /// </summary>
    public class WorkingFieldProfile : Profile
    {
        public WorkingFieldProfile()
        {
            this.CreateMap<WorkingField, WorkingFieldListDto>();
            this.CreateMap<WorkingField, WorkingFieldDto>();
            this.CreateMap<WorkingFieldModel, WorkingField>()
                .ForMember(dest => dest.WorkingDayId, opt => opt.MapFrom(src => src.WorkingDayId))
                .ForMember(dest => dest.WorkingDay, opt => opt.Ignore());
            this.CreateMap<List<WorkingField>, WorkingMonthStatDto>()
                .ForMember(dest => dest.Fields, opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.HourSum, opt => opt.MapFrom(src => this.CalcSumHour(src)))
                .ForMember(dest => dest.HourAvg, opt => opt.MapFrom(src => this.CalcAvgHour(src)))
                .ForMember(dest => dest.ActiveDays, opt => opt.MapFrom(src => this.ActiveDays(src)))
                .ForMember(dest => dest.WorkDays, opt => opt.MapFrom(src => this.WorkDays(src)))
                .ForMember(dest => dest.Counts, opt => opt.MapFrom(src => this.CalcCounts(src)));
            this.CreateMap<List<WorkingField>, WorkingWeekStatDto>()
                .ForMember(dest => dest.Fields, opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.HourSum, opt => opt.MapFrom(src => this.CalcSumHour(src)))
                .ForMember(dest => dest.HourAvg, opt => opt.MapFrom(src => this.CalcAvgHour(src)))
                .ForMember(dest => dest.ActiveDays, opt => opt.MapFrom(src => this.ActiveDays(src)))
                .ForMember(dest => dest.WorkDays, opt => opt.MapFrom(src => this.WorkDays(src)))
                .ForMember(dest => dest.Counts, opt => opt.MapFrom(src => this.CalcCounts(src)));
        }

        /// <summary>
        /// Calculates sum of hours
        /// </summary>
        /// <param name="fields">Working fields</param>
        /// <returns>Sum of hours</returns>
        private decimal CalcSumHour(List<WorkingField> fields)
        {
            return fields == null || !fields.Any()
                ? 0
                : fields.Where(x => x.WorkingDay.Type.DayIsActive).Sum(x => x.Length);
        }

        /// <summary>
        /// Calculates avg of hours
        /// </summary>
        /// <param name="fields">Working fields</param>
        /// <returns>Avg of hours</returns>
        private double CalcAvgHour(List<WorkingField> fields)
        {
            return fields == null || !fields.Any() ? 0 : (double)this.CalcSumHour(fields) / this.ActiveDays(fields);
        }

        /// <summary>
        /// Count of active days
        /// </summary>
        /// <param name="fields">Working fields</param>
        /// <returns>Count of days</returns>
        private int ActiveDays(List<WorkingField> fields)
        {
            return fields.GroupBy(x => x.WorkingDay).Select(x => x.Key).Count(x => x.Type.DayIsActive);
        }

        /// <summary>
        /// Count of work days
        /// </summary>
        /// <param name="fields">Working fields</param>
        /// <returns>Count of work days</returns>
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
            int monthDays = DateTime.DaysInMonth(year, month);

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

        /// <summary>
        /// Counts of all type of day
        /// </summary>
        /// <param name="fields">Working fields</param>
        /// <returns>List of calculated elements</returns>
        private List<WorkingDayTypeCountDto> CalcCounts(List<WorkingField> fields)
        {
            return fields.GroupBy(x => x.WorkingDay.Type).Select(x => new WorkingDayTypeCountDto
            { Type = x.Key.Title, Count = x.GroupBy(y => y.WorkingDay).Count() }).ToList();
        }
    }
}