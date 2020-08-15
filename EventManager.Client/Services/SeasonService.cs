using System.Collections.Generic;
using System.Threading.Tasks;
using EventManager.Client.Http;
using EventManager.Client.Models;
using EventManager.Client.Services.Interfaces;
using ManagerAPI.Shared.DTOs.SL;
using ManagerAPI.Shared.Models.SL;

namespace EventManager.Client.Services
{
    public class SeasonService : HttpCall<SeasonListDto, SeasonDto, SeasonModel>, ISeasonService
    {
        public SeasonService(IHttpService httpService) : base(httpService, $"{ApplicationSettings.BaseApiUrl}/season", "Season")
        {
        }

        public async Task<bool> UpdateSeenStatus(List<SeasonSeenStatusModel> models)
        {
            var settings = new HttpSettings($"{this.Url}/map/status", null, null, "My Season seen status updating");

            var body = new HttpBody<List<SeasonSeenStatusModel>>(models);

            return await this.Http.Update<List<SeasonSeenStatusModel>>(settings, body);
        }
        
        public async Task<bool> AddIncremented(int seriesId)
        {
            var pathParams = new HttpPathParameters();
            pathParams.Add<int>(seriesId, -1);
            
            var settings = new HttpSettings($"{this.Url}", null, pathParams, "Season adding");
            
            var body = new HttpBody<object>(null);

            return await this.Http.Create<object>(settings, body);
        }

        public async Task<bool> DeleteDecremented(int seasonId)
        {
            var pathParams = new HttpPathParameters();
            pathParams.Add<int>(seasonId, -1);
            
            var settings = new HttpSettings($"{this.Url}/decremented", null, pathParams, "Season deleting");

            return await this.Http.Delete(settings);
        }
    }
}