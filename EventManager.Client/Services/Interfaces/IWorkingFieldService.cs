using System.Threading.Tasks;
using ManagerAPI.Shared.DTOs.WM;
using ManagerAPI.Shared.Models.WM;

namespace EventManager.Client.Services.Interfaces
{
    public interface IWorkingFieldService
    {
        Task<bool> AddWorkingField(WorkingFieldModel model);
        Task<bool> DeleteWorkingField(int workingFieldId);
        Task<bool> UpdateWorkingField(int workingFieldId, WorkingFieldModel model);
        Task<WorkingFieldDto> GetWorkingField(int id);
    }
}