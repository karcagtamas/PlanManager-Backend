using System.Collections.Generic;
using ManagerAPI.Models.DTOs.PM;

namespace PlanManager.Services.Services.Interfaces
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