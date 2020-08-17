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
    public class MovieService : Repository<Movie, StatusLibraryNotificationType>, IMovieService
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
        public MovieService(DatabaseContext context, IMapper mapper, IUtilsService utilsService,
            ILoggerService loggerService, INotificationService notificationService) : base(context, loggerService,
            utilsService, notificationService, mapper, "Movie",
            new NotificationArguments
            {
                DeleteArguments = new List<string> {"Title"}, UpdateArguments = new List<string> {"Title"},
                CreateArguments = new List<string> {"Title"}
            })
        {
            this._databaseContext = context;
        }

        public List<MyMovieListDto> GetMyList()
        {
            var user = this.Utils.GetCurrentUser();

            var list = user.MyMovies.Where(x => x.IsAdded).ToList();

            this.Logger.LogInformation(user, this.GetService(), this.GetEvent("get my"),
                list.Select(x => x.Movie.Id).ToList());

            return Mapper.Map<List<MyMovieListDto>>(list);
        }

        public MyMovieDto GetMy(int id)
        {
            var user = this.Utils.GetCurrentUser();

            var movie = this.Get<MyMovieDto>(id);
            var myMovie = user.MyMovies.FirstOrDefault(x => x.Movie.Id == movie.Id);
            movie.IsMine = myMovie?.IsAdded ?? false;;
            movie.IsSeen = myMovie?.IsSeen ?? false;
            movie.AddedOn = myMovie?.AddedOn;
            movie.SeenOn = myMovie?.SeenOn;

            this.Logger.LogInformation(user, this.GetService(), this.GetEvent("get my"), movie.Id);

            return movie;
        }

        public void UpdateSeenStatus(int id, bool seen)
        {
            var user = this.Utils.GetCurrentUser();

            var userMovie = _databaseContext.UserMovieSwitch.Find(user.Id, id);
            if (userMovie == null)
            {
                throw this.Logger.LogInvalidThings(user, this.GetService(), UserMovieThing,
                    UserMovieConnectionDoesNotExistMessage);
            }

            userMovie.IsSeen = seen;
            userMovie.SeenOn = seen ? (DateTime?) DateTime.Now : null;
            _databaseContext.UserMovieSwitch.Update(userMovie);
            _databaseContext.SaveChanges();

            this.Logger.LogInformation(user, this.GetService(), this.GetEvent("set seen status for"),
                userMovie.Movie.Id);
            this.Notification.AddStatusLibraryNotificationByType(StatusLibraryNotificationType.MovieSeenStatusUpdated, user,
                userMovie.Movie.Title, seen ? "Seen" : "Unseen");
        }


        public void UpdateMyMovies(List<int> ids)
        {
            var user = this.Utils.GetCurrentUser();

            var currentMappings = _databaseContext.UserMovieSwitch.Where(x => x.UserId == user.Id).ToList();
            foreach (var i in currentMappings)
            {
                if (ids.FindIndex(x => x == i.MovieId) == -1)
                {
                    i.IsAdded = false;
                    i.AddedOn = null;
                    this._databaseContext.UserMovieSwitch.Update(i);
                }
                else
                {
                    i.IsAdded = true;
                    i.AddedOn = DateTime.Now;
                    this._databaseContext.UserMovieSwitch.Update(i);
                }
            }

            foreach (var i in ids)
            {
                if (currentMappings.FirstOrDefault(x => x.MovieId == i) == null)
                {
                    var map = new UserMovie() {MovieId = i, UserId = user.Id, IsSeen = false, AddedOn = DateTime.Now, IsAdded = true};
                    _databaseContext.UserMovieSwitch.Add(map);
                }
            }

            _databaseContext.SaveChanges();
            this.Logger.LogInformation(user, this.GetService(), this.GetEvent("update my"), ids);
            this.Notification.AddStatusLibraryNotificationByType(StatusLibraryNotificationType.MyMovieListUpdated, user);
        }

        public void AddMovieToMyMovies(int id)
        {
            var user = this.Utils.GetCurrentUser();

            var mapping =
                this._databaseContext.UserMovieSwitch.FirstOrDefault(x => x.UserId == user.Id && x.MovieId == id);

            if (mapping == null)
            {
                mapping = new UserMovie {MovieId = id, UserId = user.Id, IsSeen = false, AddedOn = DateTime.Now, IsAdded = true};
                this._databaseContext.UserMovieSwitch.Add(mapping);
                this._databaseContext.SaveChanges();
                this.Logger.LogInformation(user, this.GetService(), this.GetEvent("add my"), id);
                this.Notification.AddStatusLibraryNotificationByType(StatusLibraryNotificationType.MyMovieListUpdated,
                    user);
            }
            else
            {
                mapping.AddedOn = DateTime.Now;
                mapping.IsAdded = true;
                this._databaseContext.UserMovieSwitch.Update(mapping);
                this.Logger.LogInformation(user, this.GetService(), this.GetEvent("add my"), id);
                this.Notification.AddStatusLibraryNotificationByType(StatusLibraryNotificationType.MyMovieListUpdated,
                    user);
            }
        }

        public void RemoveMovieFromMyMovies(int id)
        {
            var user = this.Utils.GetCurrentUser();

            var mapping =
                this._databaseContext.UserMovieSwitch.FirstOrDefault(x => x.UserId == user.Id && x.MovieId == id);

            if (mapping != null)
            {
                mapping.IsAdded = false;
                mapping.AddedOn = null;
                this._databaseContext.UserMovieSwitch.Update(mapping);
                this._databaseContext.SaveChanges();
                this.Logger.LogInformation(user, this.GetService(), this.GetEvent("delete my"), id);
                this.Notification.AddStatusLibraryNotificationByType(StatusLibraryNotificationType.MyMovieListUpdated,
                    user);
            }
        }

        public List<MyMovieSelectorListDto> GetMySelectorList(bool onlyMine)
        {
            var user = this.Utils.GetCurrentUser();

            var list = this.GetAll<MyMovieSelectorListDto>();
            foreach (var t in list)
            {
                var myMovie = user.MyMovies.FirstOrDefault(x => x.Movie.Id == t.Id);
                t.IsMine = myMovie?.IsAdded ?? false;
                t.IsSeen = myMovie != null && myMovie.IsSeen;
            }

            if (onlyMine)
            {
                list = list.Where(x => x.IsMine).ToList();
            }

            this.Logger.LogInformation(user, this.GetService(), this.GetEvent("get my selector"),
                list.Select(x => x.Id).ToList());

            return list;
        }

        public void UpdateImage(int id, MovieImageModel model)
        {
            var user = this.Utils.GetCurrentUser();

            var movie = this._databaseContext.Movies.Find(id);

            if (movie == null) return;

            this.Mapper.Map(model, movie);

            this.Update(movie);
        }

        public void UpdateCategories(int id, MovieCategoryUpdateModel model)
        {
            var user = this.Utils.GetCurrentUser();

            var movie = this._databaseContext.Movies.Find(id);

            if (movie == null) return;

            var currentMappings = movie.Categories;

            foreach (var mapping in currentMappings)
            {
                if (!model.Ids.Contains(mapping.Category.Id))
                {
                    this._databaseContext.MovieMovieCategorySwitch.Remove(mapping);
                }
            }

            var addList = model.Ids.Where(x =>
                !currentMappings.Select(y => y.Category.Id).Contains(x)).ToList();
            
            foreach (var modelId in addList)
            {
                this._databaseContext.MovieMovieCategorySwitch.Add(new MovieMovieCategory
                    {CategoryId = modelId, MovieId = movie.Id});
            }

            this._databaseContext.SaveChanges();
            this.Logger.LogInformation(user, this.GetService(), this.GetEvent("update"), movie.Id);
            this.Notification.AddStatusLibraryNotificationByType(StatusLibraryNotificationType.UpdateMovie, user);
        }

        public void UpdateRate(int id, MovieRateModel model)
        {
            var user = this.Utils.GetCurrentUser();

            var map = user.MyMovies.FirstOrDefault(x => x.Movie.Id == id);

            if (map != null)
            {
                map.Rate = model.Rate;
                _databaseContext.UserMovieSwitch.Update(map);
                _databaseContext.SaveChanges();
                this.Logger.LogInformation(user, this.GetService(), this.GetEvent("rate"), map.Movie.Id);
            }
            else
            {
                map = new UserMovie
                    {UserId = user.Id, MovieId = id, IsAdded = false, IsSeen = false, Rate = model.Rate};
                this._databaseContext.UserMovieSwitch.Add(map);
                this._databaseContext.SaveChanges();
                this.Logger.LogInformation(user, this.GetService(), this.GetEvent("rate"), id);
            }
        }
    }
}