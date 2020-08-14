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
    public class MovieService : Repository<Movie, MovieCornerNotificationType>, IMovieService
    {
        // Things
        private const string UserMovieThing = "user-movie";

        // Messages
        private const string UserMovieConnectionDoesNotExistMessage = "User Movie connection does not exist";

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
        public MovieService(DatabaseContext context, IMapper mapper, IUtilsService utilsService, ILoggerService loggerService, INotificationService notificationService) : base(context, loggerService, utilsService, notificationService, mapper, "Movie", new NotificationArguments { DeleteArguments = new List<string> { "Title" }, UpdateArguments = new List<string> { "Title" }, CreateArguments = new List<string> { "Title" } })
        {
            this._databaseContext = context;
        }

        public List<MyMovieListDto> GetMyList()
        {
            var user = this.Utils.GetCurrentUser();

            var list = user.MyMovies.ToList();

            this.Logger.LogInformation(user, this.GetService(), this.GetEvent("get my"), list.Select(x => x.Movie.Id).ToList());

            return Mapper.Map<List<MyMovieListDto>>(list);
        }

        public MyMovieDto GetMy(int id)
        {
            var user = this.Utils.GetCurrentUser();

            var movie = this.Get<MyMovieDto>(id);
            var myMovie = user.MyMovies.FirstOrDefault(x => x.Movie.Id == movie.Id);
            movie.IsMine = myMovie != null;
            movie.IsSeen = myMovie != null && myMovie.Seen;

            this.Logger.LogInformation(user, this.GetService(), this.GetEvent("get my"), movie.Id);

            return movie;
        }

        public void UpdateSeenStatus(int id, bool seen)
        {
            var user = this.Utils.GetCurrentUser();

            var userMovie = _databaseContext.UserMovieSwitch.Find(user.Id, id);
            if (userMovie == null)
            {
                throw this.Logger.LogInvalidThings(user, this.GetService(), UserMovieThing, UserMovieConnectionDoesNotExistMessage);
            }

            userMovie.Seen = seen;
            userMovie.SeenOn = seen ? (DateTime?)DateTime.Now : null;
            _databaseContext.UserMovieSwitch.Update(userMovie);
            _databaseContext.SaveChanges();

            this.Logger.LogInformation(user, this.GetService(), this.GetEvent("set seen status for"), userMovie.Movie.Id);
            this.Notification.AddMovieCornerNotificationByType(MovieCornerNotificationType.MovieSeenStatusUpdated, user, userMovie.Movie.Title, seen ? "Seen" : "Unseen");
        }


        public void UpdateMyMovies(List<int> ids)
        {
            var user = this.Utils.GetCurrentUser();

            var currentMappings = _databaseContext.UserMovieSwitch.Where(x => x.UserId == user.Id).ToList();
            foreach (var i in currentMappings)
            {
                if (ids.FindIndex(x => x == i.MovieId) == -1)
                {
                    _databaseContext.UserMovieSwitch.Remove(i);
                }
            }

            foreach (var i in ids)
            {
                if (currentMappings.FirstOrDefault(x => x.MovieId == i) == null)
                {
                    var map = new UserMovie() { MovieId = i, UserId = user.Id, Seen = false };
                    _databaseContext.UserMovieSwitch.Add(map);
                }
            }

            _databaseContext.SaveChanges();
            this.Logger.LogInformation(user, this.GetService(), this.GetEvent("update my"), ids);
            this.Notification.AddMovieCornerNotificationByType(MovieCornerNotificationType.MyMovieListUpdated, user);
        }

        public void AddMovieToMyMovies(int id)
        {
            var user = this.Utils.GetCurrentUser();

            var mapping = this._databaseContext.UserMovieSwitch.FirstOrDefault(x => x.UserId == user.Id && x.MovieId == id);

            if (mapping == null)
            {
                mapping = new UserMovie { MovieId = id, UserId = user.Id, Seen = false };
                this._databaseContext.UserMovieSwitch.Add(mapping);
                this._databaseContext.SaveChanges();
                this.Logger.LogInformation(user, this.GetService(), this.GetEvent("add my"), id);
                this.Notification.AddMovieCornerNotificationByType(MovieCornerNotificationType.MyMovieListUpdated, user);
            }
        }

        public void RemoveMovieFromMyMovies(int id)
        {
            var user = this.Utils.GetCurrentUser();

            var mapping = this._databaseContext.UserMovieSwitch.FirstOrDefault(x => x.UserId == user.Id && x.MovieId == id);

            if (mapping != null)
            {
                this._databaseContext.UserMovieSwitch.Remove(mapping);
                this._databaseContext.SaveChanges();
                this.Logger.LogInformation(user, this.GetService(), this.GetEvent("delete my"), id);
                this.Notification.AddMovieCornerNotificationByType(MovieCornerNotificationType.MyMovieListUpdated, user);
            }
        }

        public List<MyMovieSelectorListDto> GetMySelectorList(bool onlyMine)
        {
            var user = this.Utils.GetCurrentUser();

            var list = this.GetAll<MyMovieSelectorListDto>();
            foreach (var t in list)
            {
                var myMovie = user.MyMovies.FirstOrDefault(x => x.Movie.Id == t.Id);
                t.IsMine = myMovie != null;
                t.IsSeen = myMovie != null && myMovie.Seen;
            }

            if (onlyMine)
            {
                list = list.Where(x => x.IsMine).ToList();
            }
            
            this.Logger.LogInformation(user, this.GetService(), this.GetEvent("get my selector"), list.Select(x => x.Id).ToList());

            return list;
        }
    }
}
