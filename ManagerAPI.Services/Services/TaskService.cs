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

        public TaskService(DatabaseContext context, IMapper mapper, IUtilsService utilsService, ILoggerService loggerService)
        {
            _context = context;
            _mapper = mapper;
            _utilsService = utilsService;
            _loggerService = loggerService;
        }

        public List<TaskDateDto> GetTasks(bool isSolved)
        {
            var user = _utilsService.GetCurrentUser();

            var list = _mapper.Map<List<TaskDateDto>>(user.Tasks.GroupBy(x => x.Deadline).OrderBy(x => x.Key).ToList());

            _loggerService.LogInformation(user, nameof(TaskService), GetTasksAction, 0);     

            return list;   
        }

        public TaskDataDto GetTask(int id)
        {
            var user = _utilsService.GetCurrentUser();

            var e = _mapper.Map<TaskDataDto>(_context.Tasks.Where(x => x.Id == id).FirstOrDefault());

            _loggerService.LogInformation(user, nameof(TaskService), GetTaskAction, id);     

            return e;
        }

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

        public int CreateTask(TaskModel model)
        {
            var user = _utilsService.GetCurrentUser();

            var taskEntity = new Task();

            _mapper.Map(model, taskEntity);

            _context.Tasks.Update(taskEntity);
            _context.SaveChanges();

            _loggerService.LogInformation(user, nameof(TaskService), CreateTaskAction, taskEntity.Id); 

            // TODO: Notification

            return taskEntity.Id;
        }
    }
}
