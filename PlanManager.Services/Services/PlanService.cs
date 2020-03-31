using System.Collections.Generic;
using PlanManager.Services.DTOs;

namespace PlanManager.Services.Services
{
    public class PlanService : IPlanService
    {
        public List<PlanListDto> GetPlans()
        {
            throw new System.NotImplementedException();
        }

        public List<PlanListDto> GetMyPlans()
        {
            throw new System.NotImplementedException();
        }

        public List<PlanDto> GetPlan(int id)
        {
            throw new System.NotImplementedException();
        }

        public List<PlanListDto> GetUserPublicPlans(string userId)
        {
            throw new System.NotImplementedException();
        }

        public void CreatePlan(PlanCreateDto model)
        {
            throw new System.NotImplementedException();
        }

        public void UpdatePlan(int id, PlanUpdateDto model)
        {
            throw new System.NotImplementedException();
        }

        public void DeletePlan(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}