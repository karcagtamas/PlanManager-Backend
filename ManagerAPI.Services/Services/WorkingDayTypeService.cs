using AutoMapper;
using ManagerAPI.DataAccess;
using ManagerAPI.Domain.Entities.WM;
using ManagerAPI.Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using ManagerAPI.Services.Common;
using ManagerAPI.Domain.Enums.WM;

namespace ManagerAPI.Services.Services
{
    public class WorkingDayTypeService : Repository<WorkingDayType, WorkingManagerNotificationType>, IWorkingDayTypeService
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
        public WorkingDayTypeService(DatabaseContext context, IMapper mapper, IUtilsService utilsService, INotificationService notificationService, ILoggerService loggerService) : base(context, loggerService, utilsService, notificationService, mapper, "Working day type", new NotificationArguments { })
        {
            this.DatabaseContext = context;
        }
    }
}
