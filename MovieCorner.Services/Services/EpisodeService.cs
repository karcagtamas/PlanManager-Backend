﻿using AutoMapper;
using ManagerAPI.DataAccess;
using ManagerAPI.Domain.Entities.SL;
using ManagerAPI.Domain.Enums.SL;
using ManagerAPI.Services.Common.Repository;
using ManagerAPI.Services.Services.Interfaces;
using ManagerAPI.Shared.DTOs.SL;
using ManagerAPI.Shared.Models.SL;
using MovieCorner.Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MovieCorner.Services.Services
{
    /// <summary>
    /// Episode service
    /// </summary>
    public class EpisodeService : Repository<Episode, StatusLibraryNotificationType>, IEpisodeService
    {
        // Injects
        private readonly DatabaseContext _databaseContext;

        /// <summary>
        /// Injector constructor
        /// </summary>
        /// <param name="context">Database context</param>
        /// <param name="mapper">Mapper</param>
        /// <param name="utilsService">Utils Service</param>
        /// <param name="loggerService">Logger Service</param>
        /// <param name="notificationService">Notification Service</param>
        public EpisodeService(DatabaseContext context, IMapper mapper, IUtilsService utilsService,
            ILoggerService loggerService, INotificationService notificationService) : base(context, loggerService,
            utilsService, notificationService, mapper, "Episode", new NotificationArguments
            {
                DeleteArguments = new List<string> { "Number" },
                UpdateArguments = new List<string> { "Number" },
                CreateArguments = new List<string> { "Number" }
            })
        {
            this._databaseContext = context;
        }

        /// <summary>
        /// Update seen status for episode
        /// </summary>
        /// <param name="id">Episode Id</param>
        /// <param name="seen">Seen status</param>
        public void UpdateSeenStatus(int id, bool seen)
        {
            var user = this.Utils.GetCurrentUser();

            var episode = this._databaseContext.Episodes.FirstOrDefault(x => x.Id == id);
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

                this._databaseContext.SaveChanges();

                this.Logger.LogInformation(user, this.GetService(), this.GetEvent("set episode seen status for"), id);
                this.Notification.AddStatusLibraryNotificationByType(
                    StatusLibraryNotificationType.EpisodeSeenStatusUpdated, user, episode.Season.Series.Title,
                    episode.Season.Number.ToString(), episode.Number.ToString(), seen ? "Seen" : "Unseen");
            }
        }

        /// <summary>
        /// Add episode to the given season.
        /// The episode number will be next number after the last episode.
        /// </summary>
        /// <param name="seasonId">Season Id</param>
        public void AddIncremented(int seasonId)
        {
            var last = this.GetList(x => x.Season.Id == seasonId).OrderBy(x => x.Number).LastOrDefault();

            int number = last?.Number + 1 ?? 1;

            var season = new Episode
            {
                Number = number,
                SeasonId = seasonId,
                Title = "[Episode Title]"
            };

            this.Add(season);
        }

        /// <summary>
        /// Delete episode by the given Id.
        /// Every continous episode's number will be decremented by one.
        /// </summary>
        /// <param name="episodeId">Episode Id</param>
        public void DeleteDecremented(int episodeId)
        {
            var season = this.Get(episodeId);
            int seasonId = season.Season.Id;
            int number = season.Number;

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

        /// <summary>
        /// Gets my object by the given Id.
        /// </summary>
        /// <param name="id">Episode Id</param>
        /// <returns>My episode object</returns>
        public MyEpisodeDto GetMy(int id)
        {
            var user = this.Utils.GetCurrentUser();

            var episode = this.Get<MyEpisodeDto>(id);
            var myEpisode = user.MyEpisodes.FirstOrDefault(x => x.Episode.Id == episode.Id);
            episode.IsMine = myEpisode != null;
            episode.IsSeen = myEpisode?.Seen ?? false;
            episode.SeenOn = myEpisode?.SeenOn;

            this.Logger.LogInformation(user, this.GetService(), this.GetEvent("get my"), episode.Id);

            return episode;
        }

        /// <summary>
        /// Updates episode's image.
        /// </summary>
        /// <param name="id">Episode Id</param>
        /// <param name="model">New image model</param>
        public void UpdateImage(int id, EpisodeImageModel model)
        {
            var user = this.Utils.GetCurrentUser();

            var episode = this._databaseContext.Episodes.Find(id);

            if (episode == null)
            {
                return;
            }

            this.Mapper.Map(model, episode);

            this.Update(episode);
        }
    }
}