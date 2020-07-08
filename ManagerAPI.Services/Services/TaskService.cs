using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ManagerAPI.DataAccess;
using ManagerAPI.Models.DTOs;
using ManagerAPI.Models.Entities;
using ManagerAPI.Models.Models;
using ManagerAPI.Services.Messages;
using ManagerAPI.Services.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace ManagerAPI.Services.Services
{
    public class TaskService : ITaskService
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        private readonly IUtilsService _utilsService;
        public TaskService(DatabaseContext context, IMapper mapper, IUtilsService utilsService)
        {
            _context = context;
            _mapper = mapper;
            _utilsService = utilsService;
        }

        public List<TaskDateDto> GetTasks(bool isSolved)
        {
            var user = _utilsService.GetCurrentUser();

            var list = _mapper.Map<List<TaskDateDto>>(user.Tasks.GroupBy(x => x.Deadline).OrderBy(x => x.Key).ToList());

            _utilsService.LogInformation(TaskMessages.TaskListGet, user);     

            return list;   
        }

        public TaskDataDto GetTask(int id)
        {
            var user = _utilsService.GetCurrentUser();

            var e = _mapper.Map<TaskDataDto>(_context.Tasks.Where(x => x.Id == id).FirstOrDefault());

            _utilsService.LogInformation(TaskMessages.TaskGet, user);     

            return e;
        }

        public void DeleteTask(int id)
        {
            var user = _utilsService.GetCurrentUser();

            var task = _context.Tasks.Find(id);

            if (task == null) {
                throw new Exception("");
            }

            _context.Tasks.Remove(task);
            _context.SaveChanges();

            // TODO: Notification

            _utilsService.LogInformation(TaskMessages.TaskRemove, user);
        }

        public void UpdateTask(TaskDataDto task)
        {
            var user = _utilsService.GetCurrentUser();

            var taskEntity = _context.Tasks.Find(task.Id);

            if (taskEntity == null) {
                throw new Exception(TaskMessages.InvalidTask);
            }

            _mapper.Map(task, taskEntity);
            taskEntity.LastUpdate = DateTime.Now;

            _context.Tasks.Update(taskEntity);
            _context.SaveChanges();

            // TODO: Notification

            _utilsService.LogInformation(TaskMessages.TaskUpdate, user); 
        }

        public int CreateTask(TaskModel model)
        {
            var user = _utilsService.GetCurrentUser();

            var taskEntity = new Task();

            _mapper.Map(model, taskEntity);

            _context.Tasks.Update(taskEntity);
            _context.SaveChanges();

            _utilsService.LogInformation(TaskMessages.TaskAdd, user); 

            // TODO: Notification

            return taskEntity.Id;
        }
    }
}
