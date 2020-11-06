using ManagerAPI.Domain.Entities.WM;
using ManagerAPI.Services.Common.Repository;
using ManagerAPI.Shared.DTOs.WM;
using System;

namespace ManagerAPI.Services.Services.Interfaces
{
    public interface IWorkingDayService : IRepository<WorkingDay>
    {
        WorkingDayListDto Get(DateTime day);
        WorkingDayStatDto Stat(int id);
    }
}