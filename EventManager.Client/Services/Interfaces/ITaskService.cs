using EventManager.Client.Models.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManager.Client.Services.Interfaces
{
    public interface ITaskService
    {
        Task<List<TaskDateDto>> GetTasks(bool? isSolved);
        Task<TaskDataDto> GetTask(int id);
        Task<bool> DeleteTask(int id);
        Task<bool> UpdateTask(TaskDataDto task);
        Task<bool> CreateTask(TaskModel model);
    }
}
