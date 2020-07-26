using System.Threading.Tasks;
using EventManager.Client.Models;
using EventManager.Client.Services.Interfaces;
using ManagerAPI.Shared.DTOs.WM;
using ManagerAPI.Shared.Models.WM;

namespace EventManager.Client.Services
{
    public class WorkingFieldService : HttpCall<WorkingFieldListDto, WorkingFieldDto, WorkingFieldModel>, IWorkingFieldService
    {
        public WorkingFieldService(IHttpService httpService, IHelperService helperService) : base(httpService, $"{ApplicationSettings.BaseApiUrl}/working-field", "Working field")
        {

        }
    }
}