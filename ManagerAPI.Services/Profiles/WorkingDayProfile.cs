using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ManagerAPI.Domain.Entities.WM;
using ManagerAPI.Shared.DTOs.WM;
using ManagerAPI.Shared.Models.WM;

namespace ManagerAPI.Services.Profiles
{
    public class WorkingDayProfile : Profile
    {
        private const int LotHour = 8;
        private const int OptimalHour = 6;
        private const int EnoughHour = 4;
        private const int HourToMin = 60;
        
        public WorkingDayProfile()
        {
            CreateMap<WorkingDay, WorkingDayListDto>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.Id));
            CreateMap<WorkingDay, WorkingDayDto>();
            CreateMap<WorkingDayModel, WorkingDay>()
                .ForMember(dest => dest.TypeId, opt => opt.MapFrom(src => src.Type))
                .ForMember(dest => dest.Type, opt => opt.Ignore())
                .ForMember(dest => dest.Day, opt => opt.MapFrom(src => src.Date));
            CreateMap<WorkingDay, WorkingDayStatDto>()
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.Type.DayIsActive))
                .ForMember(dest => dest.SumMinutes, opt => opt.MapFrom(src => GetSumMinutes(src.WorkingFields.ToList())))
                .ForMember(dest => dest.IsALot, opt => opt.MapFrom(src => GetSumMinutes(src.WorkingFields.ToList()) >= LotHour * HourToMin))
                .ForMember(dest => dest.IsOptimal, opt => opt.MapFrom(src => GetSumMinutes(src.WorkingFields.ToList()) >= OptimalHour * HourToMin))
                .ForMember(dest => dest.IsEnough, opt => opt.MapFrom(src => GetSumMinutes(src.WorkingFields.ToList()) >= EnoughHour * HourToMin));
        }
        
        private int GetSumMinutes(List<WorkingField> fields) 
        {
            return fields.Sum(x => (int)(x.Length * HourToMin));
        }
    }
}