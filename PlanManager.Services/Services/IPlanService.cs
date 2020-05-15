using System.Collections.Generic;
using ManagerAPI.Models.DTOs;

namespace PlanManager.Services.Services
{
    public interface IPlanService
    {
        List<PlanListDto> GetPlans();
        List<PlanListDto> GetMyPlans();
        PlanDto GetPlan(int id);
        List<PlanListDto> GetUserPublicPlans(string userId);
        void CreatePlan(PlanCreateDto model);
        void UpdatePlan(int id, PlanUpdateDto model);
        void DeletePlan(int id);
    }
}