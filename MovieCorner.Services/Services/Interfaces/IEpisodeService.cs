using ManagerAPI.Domain.Entities.MC;
using ManagerAPI.Services.Common;
using ManagerAPI.Services.Services.Interfaces;

namespace MovieCorner.Services.Services.Interfaces
{
    public interface IEpisodeService : IRepository<Episode>
    {
        void UpdateSeenStatus(int id, bool seen);
    }
}