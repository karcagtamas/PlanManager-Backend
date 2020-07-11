using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ManagerAPI.DataAccess;
using ManagerAPI.Models.DTOs;
using ManagerAPI.Models.Entities;
using ManagerAPI.Models.Models;
using ManagerAPI.Services.Services.Interfaces;

namespace ManagerAPI.Services.Services
{
    /// <summary>
    /// Task Service
    /// </summary>
    public class TaskService : ITaskService
    {
        // Action
        private const string CreateTaskAction = "create task";
        private const string UpdateTaskAction = "update task";
        private const string DeleteTaskAction = "delete task";
        private const string GetTaskAction = "get task";
        private const string GetTasksAction = "get tasks";

        // Thing
        private const string TaskThing = "task";
        private const string TaskIdThing = "task id";

        // Message
        private const string NotCorrectUpdateObjectMessage = "Not correct update object";
        private const string TaskDoesNotExistMessage = "Task does not exist";

        // Injects
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        private readonly IUtilsService _utilsService;
        private readonly ILoggerService _loggerService;

        /// <summary>
        /// Task Service
        /// </summary>
        /// <param name="context">Databaes Context</param>
        /// <param name="mapper">Mapper</param>
        /// <param name="utilsService">Utils Service</param>
        /// <param name="loggerService">Logger Service</param>
        public TaskService(DatabaseContext context, IMapper mapper, IUtilsService utilsService, ILoggerService loggerService)
        {
            _context = context;
            _mapper = mapper;
            _utilsService = utilsService;
            _loggerService = loggerService;
        }

        /// <summary>
        /// Get tasks for the current user
        /// </summary>
        /// <param name="isSolved">Filter - task is solved or not</param>
        /// <returns>List of tasks grouped by the deadline</returns>
        public List<TaskDateDto> GetTasks(bool? isSolved)
        {
            var user = _utilsService.GetCurrentUser();
            var list = _mapper.Map<List<TaskDateDto>>(user.Tasks.GroupBy(x => this.ToDay(x.Deadline)).OrderBy(x => x.Key).ToList());
            
            if (isSolved != null)
            {
                list = list.Select(x => { x.TaskList = x.TaskList.Where(y => y.IsSolved == isSolved).ToList(); return x; }).Where(x => x.TaskList.Count > 0).ToList();
            }

            _loggerService.LogInformation(user, nameof(TaskService), GetTasksAction, list.Select(x => string.Join(", ", x.TaskList.Select(y => y.Id.ToString()))).ToList());     

            return list;   
        }

        /// <summary>
        /// Get task by the given Id
        /// </summary>
        /// <param name="id">Id of the task</param>
        /// <returns>Task</returns>
        public TaskDataDto GetTask(int id)
        {
            var user = _utilsService.GetCurrentUser();

            var e = _mapper.Map<TaskDataDto>(_context.Tasks.Where(x => x.Id == id).FirstOrDefault());

            _loggerService.LogInformation(user, nameof(TaskService), GetTaskAction, id);     

            return e;
        }

        /// <summary>
        /// Delete task by the given Id
        /// </summary>
        /// <param name="id">Id of the task</param>
        public void DeleteTask(int id)
        {
            var user = _utilsService.GetCurrentUser();

            var task = _context.Tasks.Find(id);

            if (task == null) {
                throw _loggerService.LogInvalidThings(user, nameof(TaskService), TaskIdThing, TaskDoesNotExistMessage);
            }

            _context.Tasks.Remove(task);
            _context.SaveChanges();

            // TODO: Notification

            _loggerService.LogInformation(user, nameof(TaskService), DeleteTaskAction, id);
        }

        /// <summary>
        /// Update task
        /// </summary>
        /// <param name="task">Update model</param>
        public void UpdateTask(TaskDataDto task)
        {
            var user = _utilsService.GetCurrentUser();

            var taskEntity = _context.Tasks.Find(task.Id);

            if (taskEntity == null) {
                throw _loggerService.LogInvalidThings(user, nameof(TaskService), TaskThing, NotCorrectUpdateObjectMessage);
            }

            _mapper.Map(task, taskEntity);
            taskEntity.LastUpdate = DateTime.Now;

            _context.Tasks.Update(taskEntity);
            _context.SaveChanges();

            // TODO: Notification

            _loggerService.LogInformation(user, nameof(TaskService), UpdateTaskAction, task.Id); 
        }

        /// <summary>
        /// Create task
        /// </summary>
        /// <param name="model">Create model</param>
        /// <returns>Id of the newly created task</returns>
        public int CreateTask(TaskModel model)
        {
            var user = _utilsService.GetCurrentUser();

            var taskEntity = new Task();

            _mapper.Map(model, taskEntity);
            taskEntity.OwnerId = user.Id;

            _context.Tasks.Add(taskEntity);
            _context.SaveChanges();

            _loggerService.LogInformation(user, nameof(TaskService), CreateTaskAction, taskEntity.Id); 

            // TODO: Notification

            return taskEntity.Id;
        }

        private DateTime ToDay(DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day);
        }
    }
}
