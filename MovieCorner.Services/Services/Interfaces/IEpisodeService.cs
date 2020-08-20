using ManagerAPI.Domain.Entities.SL;
using ManagerAPI.Services.Common.Repository;
using ManagerAPI.Shared.DTOs.SL;
using ManagerAPI.Shared.Models.SL;

namespace MovieCorner.Services.Services.Interfaces
{
    public interface IEpisodeService : IRepository<Episode>
    {
        void UpdateSeenStatus(int id, bool seen);
        void AddIncremented(int seasonId);
        void DeleteDecremented(int episodeId);
        MyEpisodeDto GetMy(int id);
        void UpdateImage(int id, EpisodeImageModel model);
    }
}