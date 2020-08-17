﻿using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ManagerAPI.DataAccess;
using ManagerAPI.Domain.Entities.SL;
using ManagerAPI.Domain.Enums.SL;
using ManagerAPI.Services.Common.Repository;
using ManagerAPI.Services.Services.Interfaces;
using MovieCorner.Services.Services.Interfaces;

namespace MovieCorner.Services.Services
{
    public class SeasonService : Repository<Season, StatusLibraryNotificationType>, ISeasonService
    {
        // Injects
        private readonly DatabaseContext _databaseContext;

        public SeasonService(DatabaseContext context, IMapper mapper, IUtilsService utilsService,
            ILoggerService loggerService, INotificationService notificationService) : base(context, loggerService,
            utilsService, notificationService, mapper, "Season",
            new NotificationArguments
            {
                DeleteArguments = new List<string> {"Number"}, UpdateArguments = new List<string> {"Number"},
                CreateArguments = new List<string> {"Number"}
            })
        {
            this._databaseContext = context;
        }

        public void UpdateSeenStatus(int id, bool seen)
        {
            var user = this.Utils.GetCurrentUser();
            var season = this._databaseContext.Seasons.Find(id);

            var episodes = _databaseContext.Episodes.Where(x => x.Season.Id == id).ToList();
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

            this.Logger.LogInformation(user, this.GetService(), this.GetEvent("set season seen status for"), id);
            this.Notification.AddStatusLibraryNotificationByType(StatusLibraryNotificationType.SeasonSeenStatusUpdated,
                user, season?.Series.Title ?? "", season?.Number.ToString() ?? "", seen ? "Seen" : "Unseen");
        }

        public void AddIncremented(int seriesId)
        {
            var last = this.GetList(x => x.Series.Id == seriesId).OrderBy(x => x.Number).LastOrDefault();

            var number = last?.Number + 1 ?? 1;

            var season = new Season
            {
                Number = number,
                SeriesId = seriesId
            };

            this.Add(season);
        }

        public void DeleteDecremented(int seasonId)
        {
            var season = this.Get(seasonId);
            var seriesId = season.Series.Id;
            var number = season.Number;

            this.Remove(seasonId);

            var seasons = this.GetList(x => x.SeriesId == seriesId).OrderBy(x => x.Number).Select(x =>
            {
                if (x.Number > number)
                {
                    x.Number--;
                }

                return x;
            }).ToList();

            this.UpdateRange(seasons);
        }
    }
}