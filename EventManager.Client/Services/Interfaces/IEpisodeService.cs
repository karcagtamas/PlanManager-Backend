using EventManager.Client.Http;
using ManagerAPI.Shared.DTOs.SL;
using ManagerAPI.Shared.Models.SL;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventManager.Client.Services.Interfaces
{
    public interface IEpisodeService : IHttpCall<EpisodeListDto, EpisodeDto, EpisodeModel>
    {
        Task<bool> UpdateSeenStatus(List<EpisodeSeenStatusModel> models);
        Task<bool> AddIncremented(int seasonId);
        Task<bool> DeleteDecremented(int episodeId);
        Task<MyEpisodeDto> GetMy(int id);
        Task<bool> UpdateShort(int id, EpisodeShortModel model);
        Task<bool> UpdateImage(int id, EpisodeImageModel model);
    }
}