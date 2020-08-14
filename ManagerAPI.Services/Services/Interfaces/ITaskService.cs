using System.Collections.Generic;
using ManagerAPI.Domain.Entities;
using ManagerAPI.Services.Common;
using ManagerAPI.Shared.DTOs;
using ManagerAPI.Shared.Models;

namespace ManagerAPI.Services.Services.Interfaces
{
    public interface ITaskService : IRepository<Task>
    {
        List<TaskDateDto> GetDate(bool? isSolved);
    }
}
