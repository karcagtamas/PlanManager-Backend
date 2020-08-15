using System.Collections.Generic;
using ManagerAPI.Domain.Entities;
using ManagerAPI.Services.Common.Repository;
using ManagerAPI.Shared.DTOs;

namespace ManagerAPI.Services.Services.Interfaces
{
    public interface ITaskService : IRepository<Task>
    {
        List<TaskDateDto> GetDate(bool? isSolved);
    }
}
