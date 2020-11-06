using AutoMapper;
using ManagerAPI.DataAccess;
using ManagerAPI.Domain.Entities;
using ManagerAPI.Domain.Enums;
using ManagerAPI.Services.Common.Repository;
using ManagerAPI.Services.Services.Interfaces;
using System.Collections.Generic;

namespace ManagerAPI.Services.Services
{
    /// <summary>
    /// News Service
    /// </summary>
    public class NewsService : Repository<News, SystemNotificationType>, INewsService
    {
        /// <summary>
        /// Injector Constructor
        /// </summary>
        /// <param name="context">Database Context</param>
        /// <param name="utilsService">Utils Service</param>
        /// <param name="notificationService">Notification Service</param>
        /// <param name="mapper">Mapper</param>
        /// <param name="loggerService">Logger Service</param>
        public NewsService(DatabaseContext context, IUtilsService utilsService,
            INotificationService notificationService, IMapper mapper, ILoggerService loggerService) : base(context,
            loggerService, utilsService, notificationService, mapper, "News",
            new NotificationArguments
            {
                DeleteArguments = new List<string> { "CurrentUser.UserName" },
                UpdateArguments = new List<string> { "CurrentUser.UserName" },
                CreateArguments = new List<string> { "CurrentUser.UserName" }
            })
        {
        }
    }
}