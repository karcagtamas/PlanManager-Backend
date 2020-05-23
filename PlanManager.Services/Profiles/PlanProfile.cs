using System.Collections.Generic;
using AutoMapper;
using ManagerAPI.Models.DTOs;
using ManagerAPI.Models.Entities;

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