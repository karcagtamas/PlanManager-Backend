using System.Collections.Generic;
using System.Threading.Tasks;
using EventManager.Client.Http;
using ManagerAPI.Shared.DTOs.MC;
using ManagerAPI.Shared.Models.MC;

namespace EventManager.Client.Services.Interfaces
{
    public interface IEpisodeService : IHttpCall<EpisodeListDto, EpisodeDto, EpisodeModel>
    {
        Task<bool> UpdateSeenStatus(List<EpisodeSeenStatusModel> models);
    }
}