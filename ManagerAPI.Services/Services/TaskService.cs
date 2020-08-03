using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ManagerAPI.DataAccess;
using ManagerAPI.Domain.Entities;
using ManagerAPI.Domain.Enums;
using ManagerAPI.Services.Common;
using ManagerAPI.Services.Services.Interfaces;
using ManagerAPI.Shared.DTOs;
using ManagerAPI.Shared.Models;

namespace ManagerAPI.Services.Services
{
    /// <summary>
    /// Task Service
    /// </summary>
    public class TaskService : Repository<Task, SystemNotificationType>, ITaskService
    {
        // Action
        private const string GetTasksAction = "get tasks";

        // Injects
        private readonly DatabaseContext _databaseContext;

        /// <summary>
        /// Task Service
        /// </summary>
        /// <param name="context">Databaes Context</param>
        /// <param name="mapper">Mapper</param>
        /// <param name="utilsService">Utils Service</param>
        /// <param name="loggerService">Logger Service</param>
        /// <param name="notificationService">Notification Service</param>
        public TaskService(DatabaseContext context, IMapper mapper, IUtilsService utilsService, ILoggerService loggerService, INotificationService notificationService)
        :base(context, loggerService, utilsService, notificationService, mapper, "Task", new NotificationArguments { })
        {
            this._databaseContext = context;
        }

        /// <summary>
        /// Get tasks for the current user
        /// </summary>
        /// <param name="isSolved">Filter - task is solved or not</param>
        /// <returns>List of tasks grouped by the deadline</returns>
        public List<TaskDateDto> GetDate(bool? isSolved)
        {
            var user = this.Utils.GetCurrentUser();
            var list = this.Mapper.Map<List<TaskDateDto>>(user.Tasks.GroupBy(x => this.ToDay(x.Deadline)).OrderBy(x => x.Key).ToList());
            
            if (isSolved != null)
            {
                list = list.Select(x => { x.TaskList = x.TaskList.Where(y => y.IsSolved == isSolved).ToList(); return x; }).Where(x => x.TaskList.Count > 0).ToList();
            }

            this.Logger.LogInformation(user, nameof(TaskService), GetTasksAction, list.Select(x => string.Join(", ", x.TaskList.Select(y => y.Id.ToString()))).ToList());     

            return list;   
        }

        private DateTime ToDay(DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day);
        }
    }
}
