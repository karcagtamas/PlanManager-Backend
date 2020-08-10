using AutoMapper;
using ManagerAPI.DataAccess;
using ManagerAPI.Domain.Entities.MC;
using ManagerAPI.Services.Services;
using ManagerAPI.Services.Services.Interfaces;
using ManagerAPI.Shared.DTOs.MC;
using MovieCorner.Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using ManagerAPI.Services.Common;
using ManagerAPI.Domain.Enums.CM;
using System.Linq;

namespace MovieCorner.Services.Services
{
    public class SeasonService : Repository<Season, MovieCornerNotificationType>, ISeasonService
    {
        // Injects
        private readonly DatabaseContext _databaseContext;

        public SeasonService(DatabaseContext context, IMapper mapper, IUtilsService utilsService, ILoggerService loggerService, INotificationService notificationService) : base(context, loggerService, utilsService, notificationService, mapper, "Season", new NotificationArguments { })
        {
            this._databaseContext = context;
        }

        public void UpdateSeenStatus(int id, bool seen)
        {
            var user = this.Utils.GetCurrentUser();

            var episodes = _databaseContext.Episodes.Where(x => x.Season.Id == id);
            foreach (var i in episodes) {
                var connection = i.ConnectedUsers.Where(x => x.User.Id == user.Id).FirstOrDefault();
                if (connection != null) {
                    connection.Seen = seen;
                    connection.SeenOn = seen ? (DateTime?)DateTime.Now : null;
                    this._databaseContext.UserEpisodeSwitch.Update(connection);
                }
                else {
                    if (seen) {
                        var userEpisode = new UserEpisode
                        {
                            UserId = user.Id,
                            EpisodeId = id, 
                            Seen = seen,
                            SeenOn = seen ? (DateTime?)DateTime.Now : null
                        };
                        this._databaseContext.UserEpisodeSwitch.Add(userEpisode);
                    }
                }
            }
            _databaseContext.SaveChanges();

            this.Logger.LogInformation(user, this.GetService(), this.GetEvent("set season seen status for"), id);
        }
    }
}
