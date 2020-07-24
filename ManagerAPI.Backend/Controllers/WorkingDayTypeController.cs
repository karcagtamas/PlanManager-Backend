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
    [Route("api/[controller]")]
    [ApiController]
    public class WorkingDayTypeController : MyController<WorkingDayType, WorkingDayTypeModel, WorkingDayTypeListDto, WorkingDayTypeDto>
    {
        protected readonly IWorkingDayTypeService WorkingDayTypeService;
        public WorkingDayTypeController(IWorkingDayTypeService workingDayTypeService, ILoggerService loggerService) : base(loggerService, workingDayTypeService)
        {
            this.WorkingDayTypeService = workingDayTypeService;
        }
    }
}
