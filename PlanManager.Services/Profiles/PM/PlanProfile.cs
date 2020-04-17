using System.Collections;
using System.Collections.Generic;
using AutoMapper;
using PlanManager.DataAccess.Entities.PM;
using PlanManager.Services.DTOs.PM;

namespace PlanManager.Services.Profiles
{
    public class PlanProfile : Profile
    {
        public PlanProfile()
        {
            CreateMap<Plan, PlanListDto>();
            CreateMap<Plan, PlanDto>();
            CreateMap<List<Plan>, List<PlanListDto>>();
            CreateMap<ICollection<Plan>, List<PlanListDto>>();
            CreateMap<PlanCreateDto, Plan>();
            CreateMap<PlanUpdateDto, Plan>();
        }
    }
}