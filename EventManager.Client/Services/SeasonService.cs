using System.Collections.Generic;
using System.Threading.Tasks;
using EventManager.Client.Http;
using EventManager.Client.Models;
using EventManager.Client.Services.Interfaces;
using ManagerAPI.Shared.DTOs.MC;
using ManagerAPI.Shared.Models.MC;

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
    }
}