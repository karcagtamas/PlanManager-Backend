using AutoMapper;
using ManagerAPI.DataAccess;
using ManagerAPI.Domain.Entities.MC;
using ManagerAPI.Services.Services;
using ManagerAPI.Services.Services.Interfaces;
using ManagerAPI.Shared.DTOs.MC;
using MovieCorner.Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using ManagerAPI.Services.Common;
using ManagerAPI.Domain.Enums.CM;

namespace MovieCorner.Services.Services
{
    public class SeasonService : Repository<Season, MovieCornerNotificationType>, ISeasonService
    {
        // Injects
        private readonly DatabaseContext DatabaseContext;

        public SeasonService(DatabaseContext context, IMapper mapper, IUtilsService utilsService, ILoggerService loggerService, INotificationService notificationService) : base(context, loggerService, utilsService, notificationService, mapper, "Season", new NotificationArguments { })
        {
            this.DatabaseContext = context;
        }
    }
}
