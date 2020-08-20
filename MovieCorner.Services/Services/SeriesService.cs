using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ManagerAPI.DataAccess;
using ManagerAPI.Domain.Entities.SL;
using ManagerAPI.Domain.Enums.SL;
using ManagerAPI.Services.Common.Repository;
using ManagerAPI.Services.Services.Interfaces;
using ManagerAPI.Shared.DTOs.SL;
using ManagerAPI.Shared.Models.SL;
using MovieCorner.Services.Services.Interfaces;

namespace MovieCorner.Services.Services
{
    public class SeriesService : Repository<Series, StatusLibraryNotificationType>, ISeriesService
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
        public SeriesService(DatabaseContext context, IMapper mapper, IUtilsService utilsService,
            ILoggerService loggerService, INotificationService notificationService) : base(context, loggerService,
            utilsService, notificationService, mapper, "Series",
            new NotificationArguments
            {
                DeleteArguments = new List<string> {"Title"}, UpdateArguments = new List<string> {"Title"},
                CreateArguments = new List<string> {"Title"}
            })
        {
            _databaseContext = context;
        }

        public void AddSeriesToMySeries(int id)
        {
            var user = this.Utils.GetCurrentUser();

            var mapping =
                this._databaseContext.UserSeriesSwitch.FirstOrDefault(x => x.UserId == user.Id && x.SeriesId == id);

            if (mapping == null)
            {
                mapping = new UserSeries {SeriesId = id, UserId = user.Id, AddedOn = DateTime.Now, IsAdded = true};
                this._databaseContext.UserSeriesSwitch.Add(mapping);
                this._databaseContext.SaveChanges();
                this.Logger.LogInformation(user, this.GetService(), this.GetEvent("add my"), id);
                this.Notification.AddStatusLibraryNotificationByType(StatusLibraryNotificationType.MySeriesListUpdated,
                    user);
            }
            else
            {
                mapping.AddedOn = DateTime.Now;
                mapping.IsAdded = true;
                this._databaseContext.UserSeriesSwitch.Update(mapping);
                this.Logger.LogInformation(user, this.GetService(), this.GetEvent("add my"), id);
                this.Notification.AddStatusLibraryNotificationByType(StatusLibraryNotificationType.MySeriesListUpdated,
                    user);
            }
        }

        public MySeriesDto GetMy(int id)
        {
            var user = this.Utils.GetCurrentUser();

            var series = this.Get<MySeriesDto>(id);
            var mySeries = user.MySeries.FirstOrDefault(x => x.Series.Id == series.Id);
            series.IsMine = mySeries?.IsAdded ?? false;
            series.AddedOn = mySeries?.AddedOn;
            series.Rate = mySeries?.Rate ?? 0;


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

            this.Logger.LogInformation(user, this.GetService(), this.GetEvent("get my"),
                list.Select(x => x.Series.Id).ToList());

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

            this.Logger.LogInformation(user, this.GetService(), this.GetEvent("get my selector"),
                list.Select(x => x.Id).ToList());

            return list;
        }

        public void UpdateImage(int id, SeriesImageModel model)
        {
            var user = this.Utils.GetCurrentUser();

            var series = this._databaseContext.Series.Find(id);

            if (series == null) return;

            this.Mapper.Map(model, series);

            this.Update(series);
        }

        public void UpdateCategories(int id, SeriesCategoryUpdateModel model)
        {
            var user = this.Utils.GetCurrentUser();

            var series = this._databaseContext.Series.Find(id);

            if (series == null) return;

            var currentMappings = series.Categories;

            foreach (var mapping in currentMappings)
            {
                if (!model.Ids.Contains(mapping.Category.Id))
                {
                    this._databaseContext.SeriesSeriesCategoriesSwitch.Remove(mapping);
                }
            }

            var addList = model.Ids.Where(x =>
                !currentMappings.Select(y => y.Category.Id).Contains(x)).ToList();

            foreach (var modelId in addList)
            {
                this._databaseContext.SeriesSeriesCategoriesSwitch.Add(new SeriesSeriesCategory
                    {CategoryId = modelId, SeriesId = series.Id});
            }

            this._databaseContext.SaveChanges();
            this.Logger.LogInformation(user, this.GetService(), this.GetEvent("update"), series.Id);
            this.Notification.AddStatusLibraryNotificationByType(StatusLibraryNotificationType.UpdateSeries, user);
        }

        public void UpdateRate(int id, SeriesRateModel model)
        {
            var user = this.Utils.GetCurrentUser();

            var map = user.MySeries.FirstOrDefault(x => x.Series.Id == id);

            if (map != null)
            {
                map.Rate = model.Rate;
                _databaseContext.UserSeriesSwitch.Update(map);
                _databaseContext.SaveChanges();
                this.Logger.LogInformation(user, this.GetService(), this.GetEvent("rate"), map.Series.Id);
            }
            else
            {
                map = new UserSeries
                    {UserId = user.Id, SeriesId = id, IsAdded = false, Rate = model.Rate};
                this._databaseContext.UserSeriesSwitch.Add(map);
                this._databaseContext.SaveChanges();
                this.Logger.LogInformation(user, this.GetService(), this.GetEvent("rate"), id);
            }
        }

        public void RemoveSeriesFromMySeries(int id)
        {
            var user = this.Utils.GetCurrentUser();

            var mapping =
                this._databaseContext.UserSeriesSwitch.FirstOrDefault(x => x.UserId == user.Id && x.SeriesId == id);

            if (mapping != null)
            {
                mapping.IsAdded = false;
                mapping.AddedOn = null;
                this._databaseContext.UserSeriesSwitch.Update(mapping);
                this._databaseContext.SaveChanges();
                this.Logger.LogInformation(user, this.GetService(), this.GetEvent("delete my"), id);
                this.Notification.AddStatusLibraryNotificationByType(StatusLibraryNotificationType.MySeriesListUpdated,
                    user);
            }
        }

        public void UpdateMySeries(List<int> ids)
        {
            var user = this.Utils.GetCurrentUser();

            var currentMappings = _databaseContext.UserSeriesSwitch.Where(x => x.UserId == user.Id).ToList();
            foreach (var i in currentMappings)
            {
                if (ids.FindIndex(x => x == i.SeriesId) == -1)
                {
                    i.IsAdded = false;
                    i.AddedOn = null;
                    this._databaseContext.UserSeriesSwitch.Update(i);
                }
                else
                {
                    i.IsAdded = true;
                    i.AddedOn = DateTime.Now;
                    this._databaseContext.UserSeriesSwitch.Update(i);
                }
            }

            foreach (var i in ids)
            {
                if (currentMappings.FirstOrDefault(x => x.SeriesId == i) == null)
                {
                    var map = new UserSeries {SeriesId = i, UserId = user.Id, AddedOn = DateTime.Now, IsAdded = true};
                    _databaseContext.UserSeriesSwitch.Add(map);
                }
            }

            _databaseContext.SaveChanges();
            this.Logger.LogInformation(user, this.GetService(), this.GetEvent("update my"), ids);
            this.Notification.AddStatusLibraryNotificationByType(StatusLibraryNotificationType.MySeriesListUpdated, user);
        }

        public void UpdateSeenStatus(int id, bool seen)
        {
            var user = this.Utils.GetCurrentUser();
            var series = this._databaseContext.Series.Find(id);

            var episodes = _databaseContext.Episodes.Where(x => x.Season.Series.Id == id).ToList();
            foreach (var i in episodes)
            {
                var connection = i.ConnectedUsers.FirstOrDefault(x => x.User.Id == user.Id);
                if (connection != null)
                {
                    connection.Seen = seen;
                    connection.SeenOn = seen ? (DateTime?) DateTime.Now : null;
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
            this.Notification.AddStatusLibraryNotificationByType(StatusLibraryNotificationType.SeriesSeenStatusUpdated,
                user, series?.Title ?? "", seen ? "Seen" : "Unseen");
        }
    }
}