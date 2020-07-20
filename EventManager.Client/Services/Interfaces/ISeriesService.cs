using System.Collections.Generic;
using System.Threading.Tasks;
using EventManager.Client.Models.SL;

namespace EventManager.Client.Services.Interfaces
{
    public interface ISeriesService
    {
        Task<List<MySeriesDto>> GetMySeries();
        Task<List<SeriesListDto>> GetAllSeries();
        Task<SeriesDto> GetSeries(int id);
        Task<bool> CreateSeries(SeriesModel model);
        Task<bool> DeleteSeries(int id);
        Task<bool> UpdateSeries(int id, SeriesModel model);
        Task<bool> AddSeason(int seriesId, SeasonModel model);
        Task<bool> DeleteSeason(int id);
        Task<bool> UpdateSeason(int id, SeasonModel model);
        Task<bool> AddEpisode(int seasonId, EpisodeModel model);
        Task<bool> DeleteEpisode(int id);
        Task<bool> UpdateEpisode(int id, EpisodeModel model);
        Task<bool> UpdateMySeries(List<int> ids);
        Task<bool> UpdateSeenStatus(int id, bool seen);
    }
}