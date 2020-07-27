using AutoMapper;
using ManagerAPI.DataAccess;
using ManagerAPI.Domain.Entities.WM;
using ManagerAPI.Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using ManagerAPI.Services.Common;

namespace ManagerAPI.Services.Services
{
    public class WorkingDayTypeService : Repository<WorkingDayType>, IWorkingDayTypeService
    {
        // Injects
        private readonly DatabaseContext DatabaseContext;

        /// <summary>
        /// Injector Constructor
        /// </summary>
        /// <param name="context">Database Context</param>
        /// <param name="mapper">Mapper</param>
        /// <param name="utilsService">Utils Service</param>
        /// <param name="loggerService">Logger Service</param>
        public WorkingDayTypeService(DatabaseContext context, IMapper mapper, IUtilsService utilsService, ILoggerService loggerService) : base(context, loggerService, utilsService, mapper)
        {
            this.DatabaseContext = context;
        }
    }
}
