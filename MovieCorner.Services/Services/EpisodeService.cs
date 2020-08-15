﻿using System;
using System.Linq;
using AutoMapper;
using ManagerAPI.DataAccess;
using ManagerAPI.Domain.Entities.MC;
using ManagerAPI.Domain.Enums.CM;
using ManagerAPI.Services.Common.Repository;
using ManagerAPI.Services.Services.Interfaces;
using ManagerAPI.Shared.DTOs.MC;
using MovieCorner.Services.Services.Interfaces;

namespace MovieCorner.Services.Services
{
    public class EpisodeService : Repository<Episode, MovieCornerNotificationType>, IEpisodeService
    {
        // Injects
        private readonly DatabaseContext _databaseContext;

        public EpisodeService(DatabaseContext context, IMapper mapper, IUtilsService utilsService, ILoggerService loggerService, INotificationService notificationService) : base(context, loggerService, utilsService, notificationService, mapper, "Episode", new NotificationArguments())
        {
            this._databaseContext = context;
        }

        public void UpdateSeenStatus(int id, bool seen)
        {
            var user = this.Utils.GetCurrentUser();

            var episode = _databaseContext.Episodes.FirstOrDefault(x => x.Id == id);
            if (episode != null)
            {
                var connection = episode.ConnectedUsers.FirstOrDefault(x => x.User.Id == user.Id);
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
                            EpisodeId = id,
                            Seen = true,
                            SeenOn = DateTime.Now
                        };
                        this._databaseContext.UserEpisodeSwitch.Add(userEpisode);
                    }
                }
                _databaseContext.SaveChanges();

                this.Logger.LogInformation(user, this.GetService(), this.GetEvent("set episode seen status for"), id);
            }
        }

        public void AddIncremented(int seasonId)
        {
            var last = this.GetList(x => x.Season.Id == seasonId).OrderBy(x => x.Number).LastOrDefault();

            var number = last?.Number + 1 ?? 1;

            var season = new Episode
            {
                Number = number,
                SeasonId = seasonId
            };

            this.Add(season);
        }

        public void DeleteDecremented(int episodeId)
        {
            var season = this.Get(episodeId);
            var seasonId = season.Season.Id;
            var number = season.Number;

            this.Remove(episodeId);

            var episodes = this.GetList(x => x.Season.Id == seasonId).OrderBy(x => x.Number).Select(x =>
            {
                if (x.Number > number)
                {
                    x.Number--;
                }

                return x;
            }).ToList();

            this.UpdateRange(episodes);
        }

        public MyEpisodeDto GetMy(int id)
        {
            var user = this.Utils.GetCurrentUser();

            var episode = this.Get<MyEpisodeDto>(id);
            var myEpisode = user.MyEpisodes.FirstOrDefault(x => x.Episode.Id == episode.Id);
            episode.IsMine = myEpisode != null;
            episode.IsSeen = myEpisode != null && myEpisode.Seen;

            this.Logger.LogInformation(user, this.GetService(), this.GetEvent("get my"), episode.Id);

            return episode;
        }
    }
}
