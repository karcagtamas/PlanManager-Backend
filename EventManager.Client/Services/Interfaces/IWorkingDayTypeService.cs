using System.Collections.Generic;
using System.Threading.Tasks;
using EventManager.Client.Models.Interfaces;
using ManagerAPI.Shared.DTOs.WM;

namespace EventManager.Client.Services.Interfaces
{
    public interface IWorkingDayTypeService : IHttpCall<WorkingDayTypeListDto, WorkingDayTypeDto, WorkingDayTypeModel>
    {
    }
}