using AutoMapper;
using ManagerAPI.DataAccess;
using ManagerAPI.Domain.Entities.PM;
using ManagerAPI.Services.Services.Interfaces;
using ManagerAPI.Shared.DTOs.PM;
using ManagerAPI.Shared.Models.PM;
using PlanManager.Services.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace PlanManager.Services.Services
{
    /// <summary>
    /// Plan Service
    /// </summary>
    public class PlanService : IPlanService
    {
        // Actions
        private const string DeletePlanAction = "delete plan";
        private const string UpdatePlanAction = "update plan";
        private const string CreatePlanAction = "create plan";
        private const string GetPlansAction = "get plans";
        private const string GetPlanAction = "get plan";

        // Things
        private const string UserThing = "user";
        private const string PlanIdThing = "plan id";

        // Messages
        private const string PlanDoesNotExistMessage = "Plan does not exist";
        private const string UserDoesNotExistMessage = "User does not exist";

        // Injects
        private readonly IMapper _mapper;
        private readonly DatabaseContext _context;
        private readonly IUtilsService _utilsService;
        private readonly ILoggerService _loggerService;

        /// <summary>
        /// Injector Constructor
        /// </summary>
        /// <param name="mapper">Mapper</param>
        /// <param name="context">Database Context</param>
        /// <param name="utilsService">Utils Service</param>
        /// <param name="loggerService">Logger Service</param>
        public PlanService(IMapper mapper, DatabaseContext context, IUtilsService utilsService, ILoggerService loggerService)
        {
            this._mapper = mapper;
            this._context = context;
            this._utilsService = utilsService;
            this._loggerService = loggerService;
        }

        /// <summary>
        /// Get all plans
        /// </summary>
        /// <returns>List of plans</returns>
        public List<PlanListDto> GetPlans()
        {
            var user = this._utilsService.GetCurrentUser();

            var plans = this._mapper.Map<List<PlanListDto>>(this._context.Plans.ToList());

            this._loggerService.LogInformation(user, nameof(PlanService), GetPlansAction, plans.Select(x => x.Id).ToList());
            return plans;
        }

        /// <summary>
        /// Get all plans for the current user
        /// </summary>
        /// <returns>List of plans</returns>
        public List<PlanListDto> GetMyPlans()
        {
            var user = this._utilsService.GetCurrentUser();

            var plans = this._mapper.Map<List<PlanListDto>>(user.Plans);

            this._loggerService.LogInformation(user, nameof(PlanService), GetPlansAction, plans.Select(x => x.Id).ToList());
            return plans;
        }

        /// <summary>
        /// Get plan by the given Id
        /// </summary>
        /// <param name="id">Id of the plan</param>
        /// <returns>Plan</returns>
        public PlanDto GetPlan(int id)
        {
            var user = this._utilsService.GetCurrentUser();
            var plan = this._context.Plans.Find(id);

            if (plan == null)
            {
                throw this._loggerService.LogInvalidThings(user, nameof(PlanService), PlanIdThing, PlanDoesNotExistMessage);
            }

            var planDto = this._mapper.Map<PlanDto>(plan);
            this._loggerService.LogInformation(user, nameof(PlanService), GetPlanAction, plan.Id);
            return planDto;
        }

        /// <summary>
        /// Get public plans for the user with the given Id
        /// </summary>
        /// <param name="userId">User's Id</param>
        /// <returns>List of public plans</returns>
        public List<PlanListDto> GetUserPublicPlans(string userId)
        {
            var user = this._utilsService.GetCurrentUser();
            var otherUser = this._context.AppUsers.Find(userId);

            if (otherUser == null)
            {
                throw this._loggerService.LogInvalidThings(user, nameof(PlanService), UserThing, UserDoesNotExistMessage);
            }

            var plans = this._mapper.Map<List<PlanListDto>>(otherUser.Plans.Where(x => x.IsPublic).ToList());

            this._loggerService.LogInformation(user, nameof(PlanService), GetPlansAction, plans.Select(x => x.Id).ToList());

            return plans;
        }

        /// <summary>
        /// Create plan
        /// </summary>
        /// <param name="model">Model of create</param>
        public void CreatePlan(PlanModel model)
        {
            var user = this._utilsService.GetCurrentUser();

            var plan = this._mapper.Map<Plan>(model);
            this._context.Plans.Add(plan);
            this._context.SaveChanges();

            this._loggerService.LogInformation(user, nameof(PlanService), CreatePlanAction, plan.Id);
        }

        /// <summary>
        /// Update plan
        /// </summary>
        /// <param name="id">Id of the plan</param>
        /// <param name="model">Mode of update</param>
        public void UpdatePlan(int id, PlanModel model)
        {
            var user = this._utilsService.GetCurrentUser();
            var plan = this._context.Plans.Find(id);

            if (plan == null)
            {
                throw this._loggerService.LogInvalidThings(user, nameof(PlanService), PlanIdThing, PlanDoesNotExistMessage);
            }

            this._mapper.Map(model, plan);
            this._context.Plans.Update(plan);
            this._context.SaveChanges();

            this._loggerService.LogInformation(user, nameof(PlanService), UpdatePlanAction, plan.Id);
        }

        /// <summary>
        /// Delete plan
        /// </summary>
        /// <param name="id"></param>
        public void DeletePlan(int id)
        {
            var user = this._utilsService.GetCurrentUser();
            var plan = this._context.Plans.Find(id);

            if (plan == null)
            {
                throw this._loggerService.LogInvalidThings(user, nameof(PlanService), PlanIdThing, PlanDoesNotExistMessage);
            }

            this._context.Plans.Remove(plan);
            this._context.SaveChanges();

            this._loggerService.LogInformation(user, nameof(PlanService), DeletePlanAction, id);
        }
    }
}