using System.Collections.Generic;
using ManagerAPI.Models.DTOs.MC;

namespace MovieCorner.Services.Services.Interfaces
{
    public interface ISeriesService
    {
        List<SeriesListDto> GetMySeries(string userId);
        List<SeriesListDto> GetAllSeries(string userId);
        void CreateSeries(SeriesDto series, string userId);
        void DeleteSeries(int seriesId, string userId);
        void UpdateSeries(int seriesId, SeriesDto series, string userId);
        void AddSeasonsToSeries(int[] nums, int seriesId, string userId);
        void DeleteSeasonsFromSeries(int[] seasonIds, string userId);
        void AddEpisodesToSeason(int[] nums, int seasonId, string userId);
        void DeleteEpisodesFromSeason(int[] episodeIds, string userId);
        void UpdateEpisode(int episodeId, EpisodeDto episode, string userId);
    }
}
