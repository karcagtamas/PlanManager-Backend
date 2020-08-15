using System.Collections.Generic;
using System.Threading.Tasks;
using EventManager.Client.Http;
using EventManager.Client.Models;
using EventManager.Client.Services.Interfaces;
using ManagerAPI.Shared.DTOs.SL;
using ManagerAPI.Shared.Models.SL;

namespace EventManager.Client.Services
{
    public class SeriesService : HttpCall<SeriesListDto, SeriesDto, SeriesModel>, ISeriesService
    {
        public SeriesService(IHttpService httpService) : base(httpService, $"{ApplicationSettings.BaseApiUrl}/series", "Series")
        {
        }

        public async Task<bool> AddSeriesToMySeries(int id)
        {
            var pathParams = new HttpPathParameters();
            pathParams.Add<int>(id, -1);
            var settings = new HttpSettings($"{this.Url}/map", null, pathParams, "Adding series to My Series");

            var body = new HttpBody<object>(null);

            return await this.Http.Create<object>(settings, body);
        }

        public async Task<MySeriesDto> GetMy(int id)
        {
            var pathParams = new HttpPathParameters();
            pathParams.Add<int>(id, -1);
            
            var settings = new HttpSettings($"{this.Url}/my", null, pathParams);

            return await this.Http.Get<MySeriesDto>(settings);
        }

        public async Task<List<MySeriesListDto>> GetMyList()
        {
            var settings = new HttpSettings($"{this.Url}/my");

            return await this.Http.Get<List<MySeriesListDto>>(settings);
        }

        public async Task<List<MySeriesSelectorListDto>> GetMySelectorList(bool onlyMine)
        {
            var queryParams = new HttpQueryParameters();
            queryParams.Add("onlyMine", onlyMine);
            
            var settings = new HttpSettings($"{this.Url}/selector", queryParams, null);
            
            return await this.Http.Get<List<MySeriesSelectorListDto>>(settings);
        }

        public async Task<bool> RemoveSeriesFromMySeries(int id)
        {
            var pathParams = new HttpPathParameters();
            pathParams.Add<int>(id, -1);
            var settings = new HttpSettings($"{this.Url}/map", null, pathParams, "Removing series from My Series");

            return await this.Http.Delete(settings);
        }

        public async Task<bool> UpdateMySeries(MySeriesModel model)
        {
            var settings = new HttpSettings($"{this.Url}/map", null, null, "My Series updating");

            var body = new HttpBody<MySeriesModel>(model);

            return await this.Http.Update<MySeriesModel>(settings, body);
        }

        public async Task<bool> UpdateSeenStatus(SeriesSeenStatusModel model)
        {
            var settings = new HttpSettings($"{this.Url}/map/status", null, null, "My Series seen status updating");

            var body = new HttpBody<SeriesSeenStatusModel>(model);

            return await this.Http.Update<SeriesSeenStatusModel>(settings, body);
        }
    }
}