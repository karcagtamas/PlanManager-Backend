using System;
using ManagerAPI.Domain.Entities.WM;
using ManagerAPI.Services.Common.Repository;
using ManagerAPI.Shared.DTOs.WM;

namespace ManagerAPI.Services.Services.Interfaces
{
    public interface IWorkingFieldService : IRepository<WorkingField>
    {
        WorkingWeekStatDto GetWeekStat(DateTime week);
        WorkingMonthStatDto GetMonthStat(int year, int month);
    }
}