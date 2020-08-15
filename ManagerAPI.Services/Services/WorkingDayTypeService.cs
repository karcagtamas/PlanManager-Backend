﻿using System.Collections.Generic;
using AutoMapper;
using ManagerAPI.DataAccess;
using ManagerAPI.Domain.Entities.WM;
using ManagerAPI.Domain.Enums.WM;
using ManagerAPI.Services.Common.Repository;
using ManagerAPI.Services.Services.Interfaces;

namespace ManagerAPI.Services.Services
{
    public class WorkingDayTypeService : Repository<WorkingDayType, WorkingManagerNotificationType>,
        IWorkingDayTypeService
    {
        // Injects
        private readonly DatabaseContext _databaseContext;

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
                CreateArguments = new List<string> {"Title"}, DeleteArguments = new List<string> {"Title"},
                UpdateArguments = new List<string> {"Title"}
            })
        {
            this._databaseContext = context;
        }
    }
}