using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ManagerAPI.DataAccess;
using ManagerAPI.Domain.Entities.MC;
using ManagerAPI.Services.Common;
using ManagerAPI.Services.Services;
using ManagerAPI.Services.Services.Interfaces;
using ManagerAPI.Shared.DTOs.MC;
using ManagerAPI.Shared.Models.MC;
using Microsoft.Extensions.Logging;
using MovieCorner.Services.Services.Interfaces;

namespace MovieCorner.Services.Services
{
    public class SeriesService : Repository<Series>, ISeriesService
    {
        // Things
        private const string SeasonThing = "season";

        // Messages
        private const string SeasonDoesNotExistMessage = "Season does not exist";

        // Injects
        private readonly DatabaseContext _context;

        /// <summary>
        /// Injector Constructor
        /// </summary>
        /// <param name="context">Database Context</param>
        /// <param name="mapper">Mapper</param>
        /// <param name="utilsService">Utils Service</param>
        /// <param name="loggerService">Logger Service</param>
        public SeriesService(DatabaseContext context, IMapper mapper, IUtilsService utilsService, ILoggerService loggerService): base(context, loggerService, utilsService, mapper, "Series")
        {
            _context = context;
        }

        public void AddEpisode(int seasonId, EpisodeModel model)
        {
            var user = this.Utils.GetCurrentUser();
            var season = this._context.Seasons.Find(seasonId);

            if (season == null) {
                throw this.Logger.LogInvalidThings(user, this.GetService(), SeasonThing, SeasonDoesNotExistMessage);
            }

            var episode = this.Mapper.Map<Episode>(model);

            season.Episodes.Add(episode);
            _context.Seasons.Update(season);
            _context.SaveChanges();

            this.Logger.LogInformation(user, this.GetService(), "add episode to", episode.Id);
        }

        public void AddSeason(int seriesId, SeasonModel model)
        {
            var user = this.Utils.GetCurrentUser();

            var series = this._context.Series.Find(seriesId);

            if (series == null) {
                throw this.Logger.LogInvalidThings(user, this.GetService(), this.Entity, this.GetEntityErrorMessage());
            }

            var season = this.Mapper.Map<Season>(model);

            series.Seasons.Add(season);
            _context.Series.Update(series);
            _context.SaveChanges();

            this.Logger.LogInformation(user, this.GetService(), "add season to", season.Id);
        }

        public List<MySeriesDto> GetMySeries()
        {
            var user = this.Utils.GetCurrentUser();

            var list = user.MySeries.ToList();

            this.Logger.LogInformation(user, this.GetService(), this.GetEvent("get my"), list.Select(x => x.Series.Id).ToList());

            return this.Mapper.Map<List<MySeriesDto>>(list);
        }

        public void UpdateMySeries(List<int> ids) {
            var user = this.Utils.GetCurrentUser();

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

            this.Logger.LogInformation(user, this.GetService(), this.GetEvent("update my"), ids);
            _context.SaveChanges();
        }
        public void UpdateSeenStatus(int id, bool seen) {
            var user = this.Utils.GetCurrentUser();

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

            this.Logger.LogInformation(user, this.GetService(), this.GetEvent("set episode seen status for"), userEpisode.Episode.Id);
        }
    }
}
