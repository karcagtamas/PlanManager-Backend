using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ManagerAPI.Domain.Entities.WM;
using ManagerAPI.Services.Services;
using ManagerAPI.Services.Services.Interfaces;
using ManagerAPI.Shared.DTOs.WM;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ManagerAPI.Backend.Controllers
{
    [Route("api/working-day-type")]
    [ApiController]
    public class WorkingDayTypeController : MyController<WorkingDayType, WorkingDayTypeModel, WorkingDayTypeListDto, WorkingDayTypeDto>
    {
        private readonly IWorkingDayTypeService _workingDayTypeService;
        public WorkingDayTypeController(IWorkingDayTypeService workingDayTypeService, ILoggerService loggerService) : base(loggerService, workingDayTypeService)
        {
            this._workingDayTypeService = workingDayTypeService;
        }
    }
}
