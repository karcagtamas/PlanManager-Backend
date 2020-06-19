﻿using AutoMapper;
using ManagerAPI.DataAccess;
using Microsoft.Extensions.Logging;

namespace MovieCorner.Services.Services
{
    public class SeriesService
    {
        private readonly DatabaseContext _context;
        private readonly ILogger<SeriesService> _logger;
        private readonly IMapper _mapper;
        public SeriesService(DatabaseContext context, IMapper mapper, ILogger<SeriesService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }
        
        /*
        public void AddEpisodesToSeason(int[] nums, int seasonId, string userId)
        {
            // Check user
            if (userId == null)
            {
                throw new Exception($"Invalid user access");
            }
            User user = _context.ApplicationUsers.Find(userId);
            if (user == null)
            {
                throw new Exception($"Invalid user access with id: {userId}");
            }

            // Check seasons
            Season season = _context.Seasons.Find(seasonId);
            if (season == null)
            {
                throw new Exception($"Invalid season with id: {seasonId}");
            }

            // Check nums
            if (nums == null)
            {
                throw new Exception($"Invalid episode nums");
            }

            // Add episodes
            foreach (var t in nums)
            {
                Episode created = new Episode()
                {
                    Number = t,
                    Description = null,
                    SeasonId = seasonId
                };
                _context.Episodes.Add(created);
            }
            _context.SaveChanges();
            foreach (var t in nums)
            {
                _logger.LogInformation($"User {user.UserName} ({user.Id}) creates episode {t} for season {season.Number} ({season.Id}) for series {season.Series.Title} ({season.Series.Id})");
            }
        }

        public void AddSeasonsToSeries(int[] nums, int seriesId, string userId)
        {
            // Check user
            if (userId == null)
            {
                throw new Exception($"Invalid user access");
            }
            User user = _context.ApplicationUsers.Find(userId);
            if (user == null)
            {
                throw new Exception($"Invalid user access with id: {userId}");
            }

            // Check series
            Series series = _context.Serieses.Find(seriesId);
            if (series == null)
            {
                throw new Exception($"Invalid season with id: {seriesId}");
            }

            // Check nums
            if (nums == null)
            {
                throw new Exception($"Invalid episode nums");
            }

            // Add season
            foreach (var t in nums)
            {
                Season created = new Season()
                {
                    Number = t,
                    SeriesId = seriesId
                };
                _context.Seasons.Add(created);
            }
            _context.SaveChanges();
            foreach (var t in nums)
            {
                _logger.LogInformation($"User {user.UserName} ({user.Id}) creates season {t} for series {series.Title} ({series.Id})");
            }
        }

        public void CreateSeries(SeriesDTO series, string userId)
        {
            // Check series
            if (series == null)
            {
                throw new Exception($"Invalid model data for series creation");
            }

            // Check user
            if (userId == null)
            {
                throw new Exception($"Invalid user access");
            }
            User user = _context.ApplicationUsers.Find(userId);
            if (user == null)
            {
                throw new Exception($"Invalid user access with id: {userId}");
            }

            // Create series
            Series created = _mapper.Map<Series>(series);
            created.CreaterId = userId;
            created.CreationTime = DateTime.Now;

            _context.Serieses.Add(created);
            _context.SaveChanges();
            _logger.LogInformation($"User {user.UserName} ({user.Id}) creates a series");
        }

        public void DeleteEpisodesFromSeason(int[] episodeIds, string userId)
        {
            // Check user
            if (userId == null)
            {
                throw new Exception($"Invalid user access");
            }
            User user = _context.ApplicationUsers.Find(userId);
            if (user == null)
            {
                throw new Exception($"Invalid user access with id: {userId}");
            }

            // Check episode ids
            if (episodeIds == null)
            {
                throw new Exception("Invalid episode list");
            }

            // Remove episodes
            _context.Episodes.RemoveRange(_context.Episodes.Where(x => episodeIds.Count(y => y == x.Id) == 1));
            _context.SaveChanges();
            _logger.LogInformation($"User {user.UserName} ({user.Id}) deletes some episode");
        }

        public void DeleteSeasonsFromSeries(int[] seasonIds, string userId)
        {
            // Check user
            if (userId == null)
            {
                throw new Exception($"Invalid user access");
            }
            User user = _context.ApplicationUsers.Find(userId);
            if (user == null)
            {
                throw new Exception($"Invalid user access with id: {userId}");
            }

            // Check season ids
            if (seasonIds == null)
            {
                throw new Exception("Invalid season list");
            }

            // Remove seasons
            _context.Seasons.RemoveRange(_context.Seasons.Where(x => seasonIds.Count(y => y == x.Id) == 1));
            _context.SaveChanges();
            _logger.LogInformation($"User {user.UserName} ({user.Id}) deletes some season");
        }

        public void DeleteSeries(int seriesId, string userId)
        {
            // Check user
            if (userId == null)
            {
                throw new Exception($"Invalid user access");
            }
            User user = _context.ApplicationUsers.Find(userId);
            if (user == null)
            {
                throw new Exception($"Invalid user access with id: {userId}");
            }

            // Check series
            Series series = _context.Serieses.Find(seriesId);
            if (series == null)
            {
                throw new Exception($"Invalid series with id: {seriesId}");
            }

            // Remove series
            _context.Serieses.Remove(series);
            _logger.LogInformation($"User {user.UserName} ({user.Id}) deletes series {series.Title} ({series.Id})");
            _context.SaveChanges();
        }

        public List<SeriesListDTO> GetAllSeries(string userId)
        {
            // Check user
            if (userId == null)
            {
                throw new Exception($"Invalid user access");
            }
            User user = _context.ApplicationUsers.Find(userId);
            if (user == null)
            {
                throw new Exception($"Invalid user access with id: {userId}");
            }

            // Get series
            _logger.LogInformation($"User {user.UserName} ({user.Id}) gets all series");
            return _context.Serieses.Select(x => _mapper.Map<SeriesListDTO>(x)).ToList();
        }

        public List<SeriesListDTO> GetMySeries(string userId)
        {
            // Check user
            if (userId == null)
            {
                throw new Exception($"Invalid user access");
            }
            User user = _context.ApplicationUsers.Find(userId);
            if (user == null)
            {
                throw new Exception($"Invalid user access with id: {userId}");
            }

            // Get my series
            var list = _context.Serieses
                .Where(x =>
                    x.Seasons
                        .Count(y => y.Episodes
                            .Count(z => z.UserEpisodes
                                .Count(ue => ue.UserId == userId) > 0) > 0) > 0)
                .Select(x => _mapper.Map<SeriesListDTO>(x))
                .ToList();
            _logger.LogInformation($"User {user.UserName} ({user.Id}) gets own series");
            return list;
        }

        public void UpdateEpisode(int episodeId, EpisodeDTO episode, string userId)
        {
            // Check user
            if (userId == null)
            {
                throw new Exception($"Invalid user access");
            }
            User user = _context.ApplicationUsers.Find(userId);
            if (user == null)
            {
                throw new Exception($"Invalid user access with id: {userId}");
            }

            // Check episode
            if (episode == null)
            {
                throw new Exception($"Invalid episode model");
            }
            if (episode.Id != episodeId)
            {
                throw new Exception($"Invalid update operation");
            }
            Episode stateEpisode = _context.Episodes.Find(episodeId);
            if (stateEpisode == null)
            {
                throw new Exception($"Invalid episode with id: {episodeId}");
            }

            // Update episode
            stateEpisode.Description = episode.Description;
            stateEpisode.Number = episode.Number;
            _context.Episodes.Update(stateEpisode);
            _context.SaveChanges();
            _logger.LogInformation($"User {user.UserName} ({user.Id}) update episode {stateEpisode.Number} ({stateEpisode.Id}) from season {stateEpisode.Season.Number} ({stateEpisode.Season.Id}) from series {stateEpisode.Season.Series.Title} ({stateEpisode.Season.Series.Id})");
        }

        public void UpdateSeries(int seriesId, SeriesDTO series, string userId)
        {
            // Check user
            if (userId == null)
            {
                throw new Exception($"Invalid user access");
            }
            User user = _context.ApplicationUsers.Find(userId);
            if (user == null)
            {
                throw new Exception($"Invalid user access with id: {userId}");
            }

            // Check series
            if (series == null)
            {
                throw new Exception($"Invalid episode model");
            }
            if (series.Id != seriesId)
            {
                throw new Exception($"Invalid update operation");
            }
            Series stateSeries = _context.Serieses.Find(seriesId);
            if (stateSeries == null)
            {
                throw new Exception($"Invalid episode with id: {seriesId}");
            }

            // Update series
            stateSeries.Description = series.Description;
            stateSeries.Title = series.Title;
            stateSeries.StartYear = series.StartYear;
            stateSeries.EndYear = series.EndYear;
            _context.Serieses.Update(stateSeries);
            _context.SaveChanges();
            _logger.LogInformation($"User {user.UserName} ({user.Id}) update series {stateSeries.Title} ({stateSeries.Id})");
        }
        
        */
    }
}