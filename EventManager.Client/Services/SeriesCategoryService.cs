using System.Collections.Generic;
using System.Threading.Tasks;
using EventManager.Client.Http;
using EventManager.Client.Models;
using EventManager.Client.Services.Interfaces;
using ManagerAPI.Shared.DTOs.SL;
using ManagerAPI.Shared.Models.SL;

namespace EventManager.Client.Services
{
    public class SeriesCategoryService : HttpCall<SeriesCategoryDto, SeriesCategoryDto, SeriesCategoryModel>, ISeriesCategoryService
    {
        public SeriesCategoryService(IHttpService http) : base(http, $"{ApplicationSettings.BaseApiUrl}/series-category", "Series Category")
        {
        }

        public async Task<List<SeriesCategorySelectorListDto>> GetSelectorList(int movieId)
        {
            var pathParams = new HttpPathParameters();
            pathParams.Add<int>(movieId, -1);
            
            var settings = new HttpSettings($"{this.Url}/selector", null, pathParams);

            return await this.Http.Get<List<SeriesCategorySelectorListDto>>(settings);
        }
    }
}