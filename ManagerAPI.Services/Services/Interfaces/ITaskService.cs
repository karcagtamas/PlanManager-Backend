using ManagerAPI.Domain.Entities;
using ManagerAPI.Services.Common.Repository;
using ManagerAPI.Shared.DTOs;
using System.Collections.Generic;

namespace ManagerAPI.Services.Services.Interfaces
{
    public interface ITaskService : IRepository<Task>
    {
        List<TaskDateDto> GetDate(bool? isSolved);
    }
}