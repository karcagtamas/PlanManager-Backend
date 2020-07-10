using System.Collections.Generic;
using ManagerAPI.Models.DTOs;
using ManagerAPI.Models.Models;

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
