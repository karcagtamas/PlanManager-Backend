using AutoMapper;
using ManagerAPI.DataAccess;
using ManagerAPI.Domain.Entities.WM;
using ManagerAPI.Domain.Enums.WM;
using ManagerAPI.Services.Common.Repository;
using ManagerAPI.Services.Services.Interfaces;
using System.Collections.Generic;

namespace ManagerAPI.Services.Services
{
    /// <summary>
    /// Working Day Type Service
    /// </summary>
    public class WorkingDayTypeService : Repository<WorkingDayType, WorkingManagerNotificationType>,
        IWorkingDayTypeService
    {
        /// <summary>
        /// Injector Constructor
        /// </summary>
        /// <param name="context">Database Context</param>
        /// <param name="mapper">Mapper</param>
        /// <param name="utilsService">Utils Service</param>
        /// <param name="notificationService">Notification Service</param>
        /// <param name="loggerService">Logger Service</param>
        public WorkingDayTypeService(DatabaseContext context, IMapper mapper, IUtilsService utilsService,
            INotificationService notificationService, ILoggerService loggerService) : base(context, loggerService,
            utilsService, notificationService, mapper, "Working day type",
            new NotificationArguments
            {
                CreateArguments = new List<string> { "Title" },
                DeleteArguments = new List<string> { "Title" },
                UpdateArguments = new List<string> { "Title" }
            })
        {
        }
    }
}