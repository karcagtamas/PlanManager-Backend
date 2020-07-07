using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ManagerAPI.DataAccess;
using ManagerAPI.Models.DTOs.PM;
using ManagerAPI.Models.Entities;
using ManagerAPI.Services.Services.Interfaces;
using PlanManager.Services.Messages;

namespace PlanManager.Services.Services
{
    public class PlanService : IPlanService
    {
        private readonly IMapper _mapper;
        private readonly DatabaseContext _context;
        private readonly IUtilsService _utilsService;
        private readonly PlanMessages _planMessages;

        public PlanService(IMapper mapper, DatabaseContext context, IUtilsService utilsService)
        {
            _mapper = mapper;
            _context = context;
            _utilsService = utilsService;
            _planMessages = new PlanMessages();
        }
        
        public List<PlanListDto> GetPlans()
        {
            var plans = _mapper.Map<List<PlanListDto>>(_context.Plans.ToList());
            var user = _utilsService.GetCurrentUser();
            _utilsService.LogInformation(_planMessages.AllPlansGet, user);
            return plans;
        }

        public List<PlanListDto> GetMyPlans()
        {
            var user = _utilsService.GetCurrentUser();
            var plans = _mapper.Map<List<PlanListDto>>(user.Plans);
            _utilsService.LogInformation(_planMessages.MyPlansGet, user);
            return plans;
        }

        public PlanDto GetPlan(int id)
        {
            var plan = _context.Plans.Find(id);
            var user = _utilsService.GetCurrentUser();
            if (plan == null)
            {
                throw new Exception(_utilsService.AddUserToMessage(_planMessages.InvalidPlanId, user));
            }
            var planDto = _mapper.Map<PlanDto>(plan);
            _utilsService.LogInformation(_planMessages.PlanGet, user);
            return planDto;
        }

        public List<PlanListDto> GetUserPublicPlans(string userId)
        {
            var user = _context.AppUsers.Find(userId);
            var plans = _mapper.Map<List<PlanListDto>>(user.Plans.Where(x => x.IsPublic).ToList());
            _utilsService.LogInformation(_planMessages.OtherPublicPlanGet, user);
            return plans;
        }

        public void CreatePlan(PlanCreateDto model)
        {
            var plan = _mapper.Map<Plan>(model);
            _context.Plans.Add(plan);
            _context.SaveChanges();
            LogPlanEvent(_planMessages.PlanCreate);
        }

        public void UpdatePlan(int id, PlanUpdateDto model)
        {
            var plan = _context.Plans.Find(id);
            _mapper.Map(model, plan);
            _context.Plans.Update(plan);
            _context.SaveChanges();
            LogPlanEvent(_planMessages.PlanUpdate);
        }

        public void DeletePlan(int id)
        {
            var plan = _context.Plans.Find(id);
            _context.Plans.Remove(plan);
            _context.SaveChanges();
            LogPlanEvent(_planMessages.PlanDelete);
        }

        private void LogPlanEvent(string message)
        {
            var user = _utilsService.GetCurrentUser();
            _utilsService.LogInformation(message, user);
        }
    }
}