using ManagerAPI.Domain.Entities.WM;
using ManagerAPI.Shared.DTOs.WM;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManagerAPI.Services.Services.Interfaces
{
    public interface IWorkingDayService : IRepository<WorkingDay>
    {
        WorkingDayListDto Get(DateTime day);
        WorkingDayStatDto Stat(int id);
    }
}
