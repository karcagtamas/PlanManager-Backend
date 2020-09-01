using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ManagerAPI.DataAccess;
using ManagerAPI.Domain.Entities;
using ManagerAPI.Domain.Entities.WM;
using ManagerAPI.Domain.Enums.WM;
using ManagerAPI.Services.Common.Repository;
using ManagerAPI.Services.Services.Interfaces;
using ManagerAPI.Shared.DTOs.WM;

namespace ManagerAPI.Services.Services
{
    /// <summary>
    /// Working Day Service
    /// </summary>
    public class WorkingDayService : Repository<WorkingDay, WorkingManagerNotificationType>, IWorkingDayService
    {
        // Injects
        private readonly DatabaseContext _databaseContext;

        /// <summary>
        /// Injector Constructor
        /// </summary>
        /// <param name="context">Database Context</param>
        /// <param name="mapper">Mapper</param>
        /// <param name="utilsService">Utils Service</param>
        /// <param name="loggerService">Logger Service</param>
        /// <param name="notificationService">Notification Service</param>
        public WorkingDayService(DatabaseContext context, IMapper mapper, IUtilsService utilsService,
            ILoggerService loggerService, INotificationService notificationService) : base(context, loggerService,
            utilsService, notificationService, mapper, "Working day",
            new NotificationArguments
            {
                CreateArguments = new List<string> {"Day"}, DeleteArguments = new List<string> {"Day"},
                UpdateArguments = new List<string> {"Day"}
            })
        {
            this._databaseContext = context;
        }

        /// <summary>
        /// Get working day by date
        /// </summary>
        /// <param name="day">Date of the day</param>
        /// <returns>Working day</returns>
        public WorkingDayListDto Get(DateTime day)
        {
            User user = this.Utils.GetCurrentUser();
            var workingDay = user.WorkingDays.FirstOrDefault(x => x.Day == day);

            if (workingDay == null)
            {
                throw this.Logger.LogInvalidThings(user, this.GetService(), this.Entity, this.GetEntityErrorMessage());
            }

            var dto = this.Mapper.Map<WorkingDayListDto>(workingDay);
            this.Logger.LogInformation(user, this.GetService(), this.GetEvent("get"), dto.Id);
            return dto;
        }

        /// <summary>
        /// Working day statistic
        /// </summary>
        /// <param name="id">Id of the working day</param>
        /// <returns>Statistic</returns>
        public WorkingDayStatDto Stat(int id)
        {
            var user = this.Utils.GetCurrentUser();
            var workingDay = this._databaseContext.WorkingDays.Find(id);

            if (workingDay == null)
            {
                throw this.Logger.LogInvalidThings(user, this.GetService(), this.Entity, this.GetEntityErrorMessage());
            }

            this.Logger.LogInformation(user, this.GetService(), this.GetEvent("get stat for"), workingDay.Id);

            return this.Mapper.Map<WorkingDayStatDto>(workingDay);
        }
    }
}