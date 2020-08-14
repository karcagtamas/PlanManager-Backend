using ManagerAPI.Domain.Entities.WM;
using ManagerAPI.Domain.Enums.WM;
using ManagerAPI.Services.Common;
using ManagerAPI.Services.Services.Interfaces;
using ManagerAPI.Shared.DTOs.WM;
using ManagerAPI.Shared.Models.WM;
using Microsoft.AspNetCore.Mvc;

namespace ManagerAPI.Backend.Controllers
{
    [Route("api/working-day-type")]
    [ApiController]
    public class WorkingDayTypeController : MyController<WorkingDayType, WorkingDayTypeModel, WorkingDayTypeListDto, WorkingDayTypeDto, WorkingManagerNotificationType>
    {
        private readonly IWorkingDayTypeService _workingDayTypeService;
        public WorkingDayTypeController(IWorkingDayTypeService workingDayTypeService, ILoggerService loggerService) : base(loggerService, workingDayTypeService)
        {
            this._workingDayTypeService = workingDayTypeService;
        }
    }
}
