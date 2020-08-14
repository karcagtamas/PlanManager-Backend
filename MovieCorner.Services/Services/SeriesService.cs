using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ManagerAPI.DataAccess;
using ManagerAPI.Domain.Entities.MC;
using ManagerAPI.Domain.Enums.CM;
using ManagerAPI.Services.Common;
using ManagerAPI.Services.Services.Interfaces;
using ManagerAPI.Shared.DTOs.MC;
using MovieCorner.Services.Services.Interfaces;

namespace MovieCorner.Services.Services
{
    public class SeriesService : Repository<Series, MovieCornerNotificationType>, ISeriesService
    {
        // Injects
        private readonly DatabaseContext _databaseContext;

        /// <summary>
        /// Injector Constructor
        /// </summary>
        /// <param name="context">Database Context</param>
        /// <param name="mapper">Mapper</param>
        /// <param name="utilsService">Utils Service</param>
        /// <param name="loggerService">Logger Service</param>
        /// <param name="notificationService"></param>
        public SeriesService(DatabaseContext context, IMapper mapper, IUtilsService utilsService, ILoggerService loggerService, INotificationService notificationService) : base(context, loggerService, utilsService, notificationService, mapper, "Series", new NotificationArguments { })
        {
            _databaseContext = context;
        }

        public void AddSeriesToMySeries(int id)
        {
            var user = this.Utils.GetCurrentUser();

            var mapping = this._databaseContext.UserSeriesSwitch.FirstOrDefault(x => x.UserId == user.Id && x.SeriesId == id);

            if (mapping == null)
            {
                mapping = new UserSeries { SeriesId = id, UserId = user.Id };
                this._databaseContext.UserSeriesSwitch.Add(mapping);
                this._databaseContext.SaveChanges();
                this.Logger.LogInformation(user, this.GetService(), this.GetEvent("add my"), id);
                this.Notification.AddMovieCornerNotificationByType(MovieCornerNotificationType.MyBookListUpdated, user);
            }
        }

        public MySeriesDto GetMy(int id)
        {
            var user = this.Utils.GetCurrentUser();

            var series = this.Get<MySeriesDto>(id);
            var mySeries = user.MySeries.FirstOrDefault(x => x.Series.Id == series.Id);
            series.IsMine = mySeries != null;
            

            foreach (var season in series.Seasons)
            {
                foreach (var episode in season.Episodes)
                {
                    var myEpisode = user.MyEpisodes.FirstOrDefault(x => x.Episode.Id == episode.Id);
                    episode.Seen = myEpisode != null && myEpisode.Seen;
                }

                season.IsSeen = season.Episodes.Select(x => x.Seen).All(x => x);
            }

            series.IsSeen = series.Seasons.SelectMany(x => x.Episodes.Select(y => y.Seen)).All(x => x);

            this.Logger.LogInformation(user, this.GetService(), this.GetEvent("get my"), series.Id);

            return series;
        }

        public List<MySeriesListDto> GetMyList()
        {
            var user = this.Utils.GetCurrentUser();

            var list = user.MySeries.ToList();

            this.Logger.LogInformation(user, this.GetService(), this.GetEvent("get my"), list.Select(x => x.Series.Id).ToList());

            return this.Mapper.Map<List<MySeriesListDto>>(list);
        }

        public List<MySeriesSelectorListDto> GetMySelectorList(bool onlyMine)
        {
            var user = this.Utils.GetCurrentUser();

            var list = this.GetAll<MySeriesSelectorListDto>();
            foreach (var t in list)
            {
                var mySeries = user.MySeries.FirstOrDefault(x => x.Series.Id == t.Id);
                t.IsMine = mySeries != null;
            }

            if (onlyMine)
            {
                list = list.Where(x => x.IsMine).ToList();
            }

            this.Logger.LogInformation(user, this.GetService(), this.GetEvent("get my selector"), list.Select(x => x.Id).ToList());

            return list;
        }

        public void RemoveSeriesFromMySeries(int id)
        {
            var user = this.Utils.GetCurrentUser();

            var mapping = this._databaseContext.UserSeriesSwitch.FirstOrDefault(x => x.UserId == user.Id && x.SeriesId == id);

            if (mapping != null)
            {
                foreach (var episode in user.MyEpisodes.Where(x => x.Episode.Season.Series.Id == mapping.Series.Id))
                {
                    this._databaseContext.UserEpisodeSwitch.Remove(episode);
                }
                this._databaseContext.UserSeriesSwitch.Remove(mapping);
                this._databaseContext.SaveChanges();
                this.Logger.LogInformation(user, this.GetService(), this.GetEvent("delete my"), id);
                this.Notification.AddMovieCornerNotificationByType(MovieCornerNotificationType.MyBookListUpdated, user);
            }
        }

        public void UpdateMySeries(List<int> ids)
        {
            var user = this.Utils.GetCurrentUser();

            var currentMappings = _databaseContext.UserSeriesSwitch.Where(x => x.UserId == user.Id).ToList();
            foreach (var i in currentMappings.Where(i => ids.FindIndex(x => x == i.SeriesId) == -1))
            {
                foreach (var episode in user.MyEpisodes.Where(x => x.Episode.Season.Series.Id == i.SeriesId))
                {
                    _databaseContext.UserEpisodeSwitch.Remove(episode);
                }
                _databaseContext.UserSeriesSwitch.Remove(i);
            }

            foreach (var map in from i in ids where currentMappings.FirstOrDefault(x => x.SeriesId == i) == null select new UserSeries() { SeriesId = i, UserId = user.Id })
            {
                _databaseContext.UserSeriesSwitch.Add(map);
            }

            this.Logger.LogInformation(user, this.GetService(), this.GetEvent("update my"), ids);
            _databaseContext.SaveChanges();
        }
        public void UpdateSeenStatus(int id, bool seen)
        {
            var user = this.Utils.GetCurrentUser();

            var episodes = _databaseContext.Episodes.Where(x => x.Season.Series.Id == id).ToList();
            foreach (var i in episodes)
            {
                var connection = i.ConnectedUsers.FirstOrDefault(x => x.User.Id == user.Id);
                if (connection != null)
                {
                    connection.Seen = seen;
                    connection.SeenOn = seen ? (DateTime?)DateTime.Now : null;
                    this._databaseContext.UserEpisodeSwitch.Update(connection);
                }
                else
                {
                    if (seen)
                    {
                        var userEpisode = new UserEpisode
                        {
                            UserId = user.Id,
                            EpisodeId = i.Id,
                            Seen = true,
                            SeenOn = DateTime.Now
                        };
                        this._databaseContext.UserEpisodeSwitch.Add(userEpisode);
                    }
                }
            }
            _databaseContext.SaveChanges();

            this.Logger.LogInformation(user, this.GetService(), this.GetEvent("set series seen status for"), id);
        }
    }
}
