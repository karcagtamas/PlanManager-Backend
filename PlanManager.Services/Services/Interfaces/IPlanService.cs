using System.Collections.Generic;
using ManagerAPI.Shared.DTOs.PM;
using ManagerAPI.Shared.Models.PM;

namespace PlanManager.Services.Services.Interfaces
{
    public interface IPlanService
    {
        List<PlanListDto> GetPlans();
        List<PlanListDto> GetMyPlans();
        PlanDto GetPlan(int id);
        List<PlanListDto> GetUserPublicPlans(string userId);
        void CreatePlan(PlanModel model);
        void UpdatePlan(int id, PlanModel model);
        void DeletePlan(int id);
    }
}