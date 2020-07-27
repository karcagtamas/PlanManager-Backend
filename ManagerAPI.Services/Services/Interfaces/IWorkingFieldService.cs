using ManagerAPI.Domain.Entities.WM;
using System;
using System.Collections.Generic;
using System.Text;
using ManagerAPI.Services.Common;
using ManagerAPI.Shared.DTOs.WM;

namespace ManagerAPI.Services.Services.Interfaces
{
    public interface IWorkingFieldService : IRepository<WorkingField>
    {
        WorkingWeekStatDto GetWeekStat(DateTime week);
        WorkingMonthStatDto GetMonthStat(int year, int month);
    }
}
