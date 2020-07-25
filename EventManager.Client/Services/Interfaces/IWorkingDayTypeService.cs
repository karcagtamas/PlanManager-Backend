using System.Collections.Generic;
using System.Threading.Tasks;
using ManagerAPI.Shared.DTOs.WM;

namespace EventManager.Client.Services.Interfaces
{
    public interface IWorkingDayTypeService
    {
        Task<List<WorkingDayTypeListDto>> GetWorkingDayTypes();
    }
}