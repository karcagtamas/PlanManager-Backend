using AutoMapper;
using ManagerAPI.DataAccess;
using ManagerAPI.Domain.Entities.WM;
using ManagerAPI.Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManagerAPI.Services.Services
{
    public class WorkingFieldService : Repository<WorkingField>, IWorkingFieldService
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
        public WorkingFieldService(DatabaseContext context, IMapper mapper, IUtilsService utilsService, ILoggerService loggerService) : base(context, loggerService, utilsService, mapper)
        {
            this.DatabaseContext = context;
        }
    }
}
