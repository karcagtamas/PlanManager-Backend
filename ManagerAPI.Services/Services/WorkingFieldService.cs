﻿using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ManagerAPI.DataAccess;
using ManagerAPI.Domain.Entities.WM;
using ManagerAPI.Domain.Enums.WM;
using ManagerAPI.Services.Common.Repository;
using ManagerAPI.Services.Services.Interfaces;
using ManagerAPI.Shared.DTOs.WM;

namespace ManagerAPI.Services.Services
{
    /// <summary>
    /// Working Field Service
    /// </summary>
    public class WorkingFieldService : Repository<WorkingField, WorkingManagerNotificationType>, IWorkingFieldService
    {
        /// <summary>
        /// Injector Constructor
        /// </summary>
        /// <param name="context">Database Context</param>
        /// <param name="mapper">Mapper</param>
        /// <param name="utilsService">Utils Service</param>
        /// <param name="notificationService">Notification Service</param>
        /// <param name="loggerService">Logger Service</param>
        public WorkingFieldService(DatabaseContext context, IMapper mapper, IUtilsService utilsService,
            INotificationService notificationService, ILoggerService loggerService) : base(context, loggerService,
            utilsService, notificationService, mapper, "Working field", new NotificationArguments
            {
                CreateArguments = new List<string> {"Length"},
                DeleteArguments = new List<string> {"WorkingDay.Day"},
                UpdateArguments = new List<string> {"WorkingDay.Day"}
            })
        {
        }

        /// <summary>
        /// Get statistic summary for the given week
        /// </summary>
        /// <param name="week">First day of the week (M)</param>
        /// <returns>Statistic</returns>
        public WorkingWeekStatDto GetWeekStat(DateTime week)
        {
            var user = this.Utils.GetCurrentUser();

            var list = this.Mapper.Map<WorkingWeekStatDto>(this.GetList(x =>
                x.WorkingDay.Day >= week && x.WorkingDay.Day <= week.AddDays(7) && x.WorkingDay.User.Id == user.Id));

            this.Logger.LogInformation(user, this.GetService(), this.GetEvent("get week stat for"),
                list.Fields.Select(x => x.Id).ToList());

            return list;
        }

        /// <summary>
        /// Get statistic summary for the given month
        /// </summary>
        /// <param name="year">Year</param>
        /// <param name="month">Month</param>
        /// <returns>Statistic</returns>
        public WorkingMonthStatDto GetMonthStat(int year, int month)
        {
            var user = this.Utils.GetCurrentUser();

            var list = this.Mapper.Map<WorkingMonthStatDto>(GetList(x =>
                x.WorkingDay.Day.Year == year && x.WorkingDay.Day.Month == month && x.WorkingDay.User.Id == user.Id));

            this.Logger.LogInformation(user, this.GetService(), this.GetEvent("get month stat for"),
                list.Fields.Select(x => x.Id).ToList());

            return list;
        }
    }
}