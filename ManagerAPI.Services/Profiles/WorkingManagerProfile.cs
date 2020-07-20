using AutoMapper;
using ManagerAPI.Domain.Entities.WM;
using ManagerAPI.Shared.DTOs.WM;
using ManagerAPI.Shared.Models.WM;
using System.Collections.Generic;
using System.Linq;

namespace ManagerAPI.Services.Profiles
{
    public class WorkingManagerProfile : Profile
    {
        private const int LOT_HOUR = 8;
        private const int OPTIMAL_HOUR = 6;
        private const int ENOUGH_HOUR = 4;
        private const int HOUR_TO_MIN = 60;

        public WorkingManagerProfile()
        {
            CreateMap<WorkingDay, WorkingDayListDto>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.Id));
            CreateMap<WorkingField, WorkingFieldListDto>();
            CreateMap<WorkingDayModel, WorkingDay>()
                .ForMember(dest => dest.TypeId, opt => opt.MapFrom(src => src.Type))
                .ForMember(dest => dest.Type, opt => opt.Ignore());
            CreateMap<WorkingFieldModel, WorkingField>()
                .ForMember(dest => dest.WorkingDayId, opt => opt.Ignore())
                .ForMember(dest => dest.WorkingDay, opt => opt.Ignore());
            CreateMap<WorkingDayType, WorkingDayTypeDto>();
            CreateMap<WorkingField, WorkingFieldDto>();
            CreateMap<WorkingDay, WorkingDayStatDto>()
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.Type.DayIsActive))
                .ForMember(dest => dest.SumMinutes, opt => opt.MapFrom(src => GetSumMinutes(src.WorkingFields.ToList())))
                .ForMember(dest => dest.IsALot, opt => opt.MapFrom(src => GetSumMinutes(src.WorkingFields.ToList()) > LOT_HOUR * HOUR_TO_MIN))
                .ForMember(dest => dest.IsOptimal, opt => opt.MapFrom(src => GetSumMinutes(src.WorkingFields.ToList()) > OPTIMAL_HOUR * HOUR_TO_MIN))
                .ForMember(dest => dest.IsEnough, opt => opt.MapFrom(src => GetSumMinutes(src.WorkingFields.ToList()) > ENOUGH_HOUR * HOUR_TO_MIN));
        }

        private int GetSumMinutes(List<WorkingField> fields) 
        {
            return fields.Sum(x => (int)(x.Length * HOUR_TO_MIN));
        }
    }
}
