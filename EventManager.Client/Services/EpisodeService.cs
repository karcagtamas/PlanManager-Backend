using System.Collections.Generic;
using System.Threading.Tasks;
using EventManager.Client.Http;
using EventManager.Client.Models;
using EventManager.Client.Services.Interfaces;
using ManagerAPI.Shared.DTOs.MC;
using ManagerAPI.Shared.Models.MC;

namespace EventManager.Client.Services
{
    public class EpisodeService : HttpCall<EpisodeListDto, EpisodeDto, EpisodeModel>, IEpisodeService
    {
        public EpisodeService(IHttpService httpService) : base(httpService, $"{ApplicationSettings.BaseApiUrl}/episode", "Episode")
        {
        }

        public async Task<bool> UpdateSeenStatus(List<EpisodeSeenStatusModel> models)
        {
            var settings = new HttpSettings($"{this.Url}/map/status", null, null, "My Episode seen status updating");

            var body = new HttpBody<List<EpisodeSeenStatusModel>>(models);

            return await this.Http.Update<List<EpisodeSeenStatusModel>>(settings, body);
        }

        public async Task<bool> AddIncremented(int seasonId)
        {
            var pathParams = new HttpPathParameters();
            pathParams.Add<int>(seasonId, -1);
            
            var settings = new HttpSettings($"{this.Url}", null, pathParams, "Episode adding");
            
            var body = new HttpBody<object>(null);

            return await this.Http.Create<object>(settings, body);
        }

        public async Task<bool> DeleteDecremented(int episodeId)
        {
            var pathParams = new HttpPathParameters();
            pathParams.Add<int>(episodeId, -1);
            
            var settings = new HttpSettings($"{this.Url}/decremented", null, pathParams, "Episode deleting");

            return await this.Http.Delete(settings);
        }
    }
}