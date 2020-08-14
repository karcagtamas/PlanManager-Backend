using EventManager.Client.Http;
using ManagerAPI.Shared.DTOs;
using ManagerAPI.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventManager.Client.Services.Interfaces
{
    public interface ITaskService : IHttpCall<TaskListDto, TaskDto, TaskModel>
    {
        Task<List<TaskDateDto>> GetDate(bool? isSolved);
    }
}
