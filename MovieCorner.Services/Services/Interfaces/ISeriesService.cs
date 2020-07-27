using ManagerAPI.Domain.Entities.MC;
using ManagerAPI.Services.Services.Interfaces;
using ManagerAPI.Shared.DTOs.MC;
using ManagerAPI.Shared.Models.MC;
using System.Collections.Generic;
using ManagerAPI.Services.Common;

namespace MovieCorner.Services.Services.Interfaces
{
    public interface ISeriesService : IRepository<Series>
    {
        List<MySeriesDto> GetMySeries();
        void AddSeason(int seriesId, SeasonModel model);
        void AddEpisode(int seasonId, EpisodeModel model);
        void UpdateMySeries(List<int> ids);
        void UpdateSeenStatus(int id, bool seen);
    }
}
