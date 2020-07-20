using EventManager.Client.Models;
using EventManager.Client.Services.Interfaces;
using ManagerAPI.Shared.DTOs;
using ManagerAPI.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventManager.Client.Services
{
    public class TaskService : ITaskService
    {
        private readonly string _url = ApplicationSettings.BaseApiUrl + "/task";
        private readonly IHelperService _helperService;
        private readonly IHttpService _httpService;

        public TaskService(IHelperService helperService, IHttpService httpService)
        {
            this._helperService = helperService;
            this._httpService = httpService;
        }

        public async Task<bool> CreateTask(TaskModel model)
        {
            var settings = new HttpSettings($"{this._url}", null, null, "Task creating");

            var body = new HttpBody<TaskModel>(_helperService, model);

            return await this._httpService.create<TaskModel>(settings, body);
        }

        public async Task<bool> DeleteTask(int id)
        {
            var pathParams = new HttpPathParameters();
            pathParams.Add<int>(id, -1);

            var settings = new HttpSettings($"{this._url}", null, pathParams, "Task deleting");

            return await this._httpService.delete(settings);
        }

        public async Task<TaskDataDto> GetTask(int id)
        {
            var pathParams = new HttpPathParameters();
            pathParams.Add<int>(id, -1);

            var settings = new HttpSettings($"{this._url}", null, pathParams);

            return await this._httpService.get<TaskDataDto>(settings);
        }

        public async Task<List<TaskDateDto>> GetTasks(bool? isSolved)
        {
            var queryParams = new HttpQueryParameters();

            if (isSolved != null)
            {
                queryParams.Add<bool>("isSolved", (bool)isSolved);
            }

            var settings = new HttpSettings($"{this._url}", queryParams, null);

            return await this._httpService.get<List<TaskDateDto>>(settings);
        }

        public async Task<bool> UpdateTask(TaskDataDto task)
        {
            var settings = new HttpSettings($"{this._url}", null, null, "Task updating");

            var body = new HttpBody<TaskDataDto>(_helperService, task);

            return await this._httpService.update<TaskDataDto>(settings, body);
        }
    }
}
