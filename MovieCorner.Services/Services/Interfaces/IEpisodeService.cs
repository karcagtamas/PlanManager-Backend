using ManagerAPI.Domain.Entities.MC;
using ManagerAPI.Services.Common;
using ManagerAPI.Shared.DTOs.MC;

namespace MovieCorner.Services.Services.Interfaces
{
    public interface IEpisodeService : IRepository<Episode>
    {
        void UpdateSeenStatus(int id, bool seen);
        void AddIncremented(int seasonId);
        void DeleteDecremented(int episodeId);
        MyEpisodeDto GetMy(int id);
    }
}