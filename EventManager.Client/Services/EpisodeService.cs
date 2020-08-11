using System.Collections.Generic;
using System.Threading.Tasks;
using EventManager.Client.Http;
using EventManager.Client.Models;
using EventManager.Client.Services.Interfaces;
using ManagerAPI.Shared.DTOs;
using ManagerAPI.Shared.DTOs.MC;
using ManagerAPI.Shared.Models;
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
    }
}