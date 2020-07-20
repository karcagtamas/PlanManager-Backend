using System.Collections.Generic;
using ManagerAPI.Shared.DTOs;
using ManagerAPI.Shared.Models;

namespace ManagerAPI.Services.Services.Interfaces
{
    public interface ITaskService
    {
        List<TaskDateDto> GetTasks(bool? isSolved);
        TaskDataDto GetTask(int id);
        void DeleteTask(int id);
        void UpdateTask(TaskDataDto task);
        int CreateTask(TaskModel model);
    }
}
