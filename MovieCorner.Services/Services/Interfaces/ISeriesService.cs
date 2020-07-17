using System.Collections.Generic;
using ManagerAPI.Models.DTOs.MC;
using ManagerAPI.Models.Models.MC;

namespace MovieCorner.Services.Services.Interfaces
{
    public interface ISeriesService
    {
        List<MySeriesDto> GetMySeries();
        List<SeriesListDto> GetAllSeries();
        SeriesDto GetSeries(int id);
        void CreateSeries(SeriesModel model);
        void DeleteSeries(int id);
        void UpdateSeries(int id, SeriesModel model);
        void AddSeason(int seriesId, SeasonModel model);
        void DeleteSeason(int id);
        void UpdateSeason(int id, SeasonModel model);
        void AddEpisode(int seasonId, EpisodeModel model);
        void DeleteEpisode(int id);
        void UpdateEpisode(int id, EpisodeModel model);
        void UpdateMySeries(List<int> ids);
        void UpdateSeenStatus(int id, bool seen);
    }
}
