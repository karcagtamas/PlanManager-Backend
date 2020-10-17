using AutoMapper;
using ManagerAPI.Domain.Entities.PM;
using ManagerAPI.Shared.DTOs.PM;
using ManagerAPI.Shared.Models.PM;
using System.Collections.Generic;

namespace PlanManager.Services.Profiles
{
    public class PlanProfile : Profile
    {
        public PlanProfile()
        {
            this.CreateMap<Plan, PlanListDto>();
            this.CreateMap<Plan, PlanDto>();
            this.CreateMap<List<Plan>, List<PlanListDto>>();
            this.CreateMap<ICollection<Plan>, List<PlanListDto>>();
            this.CreateMap<PlanModel, Plan>();
        }
    }
}