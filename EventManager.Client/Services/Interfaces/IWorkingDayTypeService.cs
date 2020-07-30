using EventManager.Client.Http;
using ManagerAPI.Shared.DTOs.WM;

namespace EventManager.Client.Services.Interfaces
{
    public interface IWorkingDayTypeService : IHttpCall<WorkingDayTypeListDto, WorkingDayTypeDto, WorkingDayTypeModel>
    {
    }
}