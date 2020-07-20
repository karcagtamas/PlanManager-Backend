using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ManagerAPI.DataAccess;
using ManagerAPI.Domain.Entities.MC;
using ManagerAPI.Services.Services.Interfaces;
using ManagerAPI.Shared.DTOs.MC;
using ManagerAPI.Shared.Models.MC;
using Microsoft.Extensions.Logging;
using MovieCorner.Services.Services.Interfaces;

namespace MovieCorner.Services.Services
{
    public class SeriesService : ISeriesService
    {
        // Actions
        private const string SetEpisodeStatusAction = "set episode status";
        private const string UpdateMySeriesAction = "update my series";
        private const string UpdateSeriesAction = "update series";
        private const string UpdateSeasonAction = "update season";
        private const string UpdateEpisodeAction = "update episode";
        private const string GetSeriesAction = "get series";
        private const string GetMySeriesAction = "get my series";
        private const string DeleteSeriesAction = "delete series";
        private const string DeleteSeasonAction = "delete season";
        private const string DeleteEpisodeAction = "delete episode";
        private const string CreateSeriesAction = "create series";
        private const string AddSeasonAction = "add season";
        private const string AddEpisodeAction = "add episode";

        // Things
        private const string SeriesThing = "series";
        private const string SeasonThing = "season";
        private const string EpisodeThing = "episode";

        // Messages
        private const string SeriesDoesNotExistMessage = "Series does not exist";
        private const string SeasonDoesNotExistMessage = "Season does not exist";
        private const string EpisodeDoesNotExistMessage = "Episode does not exist";

        // Injects
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        private readonly IUtilsService _utilsService;
        private readonly ILoggerService _loggerService;

        /// <summary>
        /// Injector Constructor
        /// </summary>
        /// <param name="context">Database Context</param>
        /// <param name="mapper">Mapper</param>
        /// <param name="utilsService">Utils Service</param>
        /// <param name="loggerService">Logger Service</param>
        public SeriesService(DatabaseContext context, IMapper mapper, IUtilsService utilsService, ILoggerService loggerService)
        {
            _context = context;
            _mapper = mapper;
            _utilsService = utilsService;
            _loggerService = loggerService;
        }

        public void AddEpisode(int seasonId, EpisodeModel model)
        {
            var user = this._utilsService.GetCurrentUser();
            var season = this._context.Seasons.Find(seasonId);

            if (season == null) {
                throw this._loggerService.LogInvalidThings(user, nameof(SeriesService), SeasonThing, SeasonDoesNotExistMessage);
            }

            var episode = this._mapper.Map<Episode>(model);

            season.Episodes.Add(episode);
            _context.Seasons.Update(season);
            _context.SaveChanges();

            this._loggerService.LogInformation(user, nameof(SeriesService), AddEpisodeAction, episode.Id);
        }

        public void AddSeason(int seriesId, SeasonModel model)
        {
            var user = this._utilsService.GetCurrentUser();

            var series = this._context.Series.Find(seriesId);

            if (series == null) {
                throw this._loggerService.LogInvalidThings(user, nameof(SeriesService), SeriesThing, SeriesDoesNotExistMessage);
            }

            var season = this._mapper.Map<Season>(model);

            series.Seasons.Add(season);
            _context.Series.Update(series);
            _context.SaveChanges();

            this._loggerService.LogInformation(user, nameof(SeriesService), AddSeasonAction, season.Id);
        }

        public void CreateSeries(SeriesModel model)
        {
            var user = this._utilsService.GetCurrentUser();

            var series = this._mapper.Map<Series>(model);
            series.CreatorId = user.Id;
            series.LastUpdaterId = user.Id;

            _context.Series.Add(series);
            _context.SaveChanges();

            this._loggerService.LogInformation(user, nameof(SeriesService), CreateSeriesAction, series.Id);
        }

        public void DeleteEpisode(int id)
        {
            var user = this._utilsService.GetCurrentUser();

            var episode = _context.Episodes.Find(id);
            if (episode == null)
            {
                throw this._loggerService.LogInvalidThings(user, nameof(SeriesService), EpisodeThing, EpisodeDoesNotExistMessage);
            }

            this._context.Episodes.Remove(episode);
            this._context.SaveChanges();

            this._loggerService.LogInformation(user, nameof(SeriesService), DeleteEpisodeAction, id);
        }

        public void DeleteSeason(int id)
        {
            var user = this._utilsService.GetCurrentUser();

            var season = _context.Seasons.Find(id);
            if (season == null)
            {
                throw this._loggerService.LogInvalidThings(user, nameof(SeriesService), SeasonThing, SeasonDoesNotExistMessage);
            }

            this._context.Seasons.Remove(season);
            this._context.SaveChanges();

            this._loggerService.LogInformation(user, nameof(SeriesService), DeleteSeasonAction, id);
        }

        public void DeleteSeries(int id)
        {
            var user = this._utilsService.GetCurrentUser();

            var series = _context.Series.Find(id);
            if (series == null)
            {
                throw this._loggerService.LogInvalidThings(user, nameof(SeriesService), SeriesThing, SeriesDoesNotExistMessage);
            }

            this._context.Series.Remove(series);
            this._context.SaveChanges();

            this._loggerService.LogInformation(user, nameof(SeriesService), DeleteSeriesAction, id);
        }

        public List<SeriesListDto> GetAllSeries()
        {
            var user = this._utilsService.GetCurrentUser();

            var list = this._context.Series.ToList();

            this._loggerService.LogInformation(user, nameof(SeriesService), GetSeriesAction, list.Select(x => x.Id).ToList());

            return this._mapper.Map<List<SeriesListDto>>(list);
        }

        public List<MySeriesDto> GetMySeries()
        {
            var user = this._utilsService.GetCurrentUser();

            var list = user.MySeries.ToList();

            this._loggerService.LogInformation(user, nameof(SeriesService), GetMySeriesAction, list.Select(x => x.Series.Id).ToList());

            return _mapper.Map<List<MySeriesDto>>(list);
        }

        public SeriesDto GetSeries(int id)
        {
            var user = this._utilsService.GetCurrentUser();

            var series = _context.Series.Find(id);
            
            if (series == null)
            {
                throw this._loggerService.LogInvalidThings(user, nameof(SeriesService), SeriesThing, SeriesDoesNotExistMessage);
            }

            this._loggerService.LogInformation(user, nameof(SeriesService), GetSeriesAction, id);

            return _mapper.Map<SeriesDto>(series);
        }

        public void UpdateEpisode(int id, EpisodeModel model)
        {
            var user = this._utilsService.GetCurrentUser();

            var episode = _context.Episodes.Find(id);
            if (episode == null)
            {
                throw this._loggerService.LogInvalidThings(user, nameof(SeriesService), EpisodeThing, EpisodeDoesNotExistMessage);
            }

            this._mapper.Map(model, episode);

            _context.Episodes.Update(episode);
            _context.SaveChanges();

            this._loggerService.LogInformation(user, nameof(SeriesService), UpdateEpisodeAction, episode.Id);
        }

        public void UpdateSeason(int id, SeasonModel model)
        {
            var user = this._utilsService.GetCurrentUser();

            var season = _context.Seasons.Find(id);
            if (season == null)
            {
                throw this._loggerService.LogInvalidThings(user, nameof(SeriesService), SeasonThing, SeasonDoesNotExistMessage);
            }

            this._mapper.Map(model, season);

            _context.Seasons.Update(season);
            _context.SaveChanges();

            this._loggerService.LogInformation(user, nameof(SeriesService), UpdateSeasonAction, season.Id);
        }

        public void UpdateSeries(int id, SeriesModel model)
        {
            var user = this._utilsService.GetCurrentUser();

            var series = _context.Series.Find(id);
            if (series == null)
            {
                throw this._loggerService.LogInvalidThings(user, nameof(SeriesService), SeriesThing, SeriesDoesNotExistMessage);
            }

            this._mapper.Map(model, series);
            series.LastUpdaterId = user.Id;
            series.LastUpdate = DateTime.Now;

            _context.Series.Update(series);
            _context.SaveChanges();

            this._loggerService.LogInformation(user, nameof(SeriesService), UpdateSeriesAction, series.Id);
        }

        public void UpdateMySeries(List<int> ids) {
            var user = this._utilsService.GetCurrentUser();

            var currentMappings = _context.UserSeriesSwitch.Where(x => x.UserId == user.Id).ToList();
            foreach (var i in currentMappings)
            {
                if (ids.FindIndex(x => x == i.SeriesId) == -1)
                {
                    _context.UserSeriesSwitch.Remove(i);
                }
            }

            foreach (var i in ids)
            {
                if (currentMappings.FirstOrDefault(x => x.SeriesId == i) == null)
                {
                    var map = new UserSeries() { SeriesId = i, UserId = user.Id };
                    _context.UserSeriesSwitch.Add(map);
                }
            }

            this._loggerService.LogInformation(user, nameof(SeriesService), UpdateMySeriesAction, ids);
            _context.SaveChanges();
        }
        public void UpdateSeenStatus(int id, bool seen) {
            var user = this._utilsService.GetCurrentUser();

            var userEpisode = _context.UserEpisodeSwitch.Find(user.Id, id);
            if (userEpisode == null)
            {
                userEpisode = new UserEpisode {
                    UserId = user.Id,
                    EpisodeId = id
                };
            }

            userEpisode.Seen = seen;
            userEpisode.SeenOn = seen ? (DateTime?)DateTime.Now : null;
            _context.UserEpisodeSwitch.Update(userEpisode);
            _context.SaveChanges();

            this._loggerService.LogInformation(user, nameof(SeriesService), SetEpisodeStatusAction, userEpisode.Episode.Id);
        }
    }
}
