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
        // Injects
        private readonly DatabaseContext DatabaseContext;

        /// <summary>
        /// Injector Constructor
        /// </summary>
        /// <param name="context">Database Context</param>
        /// <param name="mapper">Mapper</param>
        /// <param name="utilsService">Utils Service</param>
        /// <param name="loggerService">Logger Service</param>
        public MovieService(DatabaseContext context, IMapper mapper, IUtilsService utilsService, ILoggerService loggerService, INotificationService notificationService) : base(context, loggerService, utilsService, notificationService, mapper, "Movie", new NotificationArguments { })
        {
            this.DatabaseContext = context;
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
            movie.IsMine = user.MyMovies.Select(x => x.Movie).Any(x => x.Id == movie.Id);
            
            this.Logger.LogInformation(user, this.GetService(), this.GetEvent("get my"), movie.Id);
            
            return movie;
        }

        public void UpdateSeenStatus(int id, bool seen)
        {
            var user = this.Utils.GetCurrentUser();

            var userMovie = DatabaseContext.UserMovieSwitch.Find(user.Id, id);
            if (userMovie == null)
            {
                throw this.Logger.LogInvalidThings(user, this.GetService(), UserMovieThing, UserMovieConnectionDoesNotExistMessage);
            }

            userMovie.Seen = seen;
            userMovie.SeenOn = seen ? (DateTime?)DateTime.Now : null;
            DatabaseContext.UserMovieSwitch.Update(userMovie);
            DatabaseContext.SaveChanges();

            this.Logger.LogInformation(user, this.GetService(), this.GetEvent("set seen status for"), userMovie.Movie.Id);
        }


        public void UpdateMyMovies(List<int> ids)
        {
            var user = this.Utils.GetCurrentUser();

            var currentMappings = DatabaseContext.UserMovieSwitch.Where(x => x.UserId == user.Id).ToList();
            foreach (var i in currentMappings)
            {
                if (ids.FindIndex(x => x == i.MovieId) == -1)
                {
                    DatabaseContext.UserMovieSwitch.Remove(i);
                }
            }

            foreach (var i in ids)
            {
                if (currentMappings.FirstOrDefault(x => x.MovieId == i) == null)
                {
                    var map = new UserMovie() { MovieId = i, UserId = user.Id, Seen = false };
                    DatabaseContext.UserMovieSwitch.Add(map);
                }
            }

            this.Logger.LogInformation(user, this.GetService(), this.GetEvent("update my"), ids);
            DatabaseContext.SaveChanges();
        }

        public void AddMovieToMyMovies(int id)
        {
            var user = this.Utils.GetCurrentUser();

            var mapping = this.DatabaseContext.UserMovieSwitch.Where(x => x.UserId == user.Id && x.MovieId == id).FirstOrDefault();

            if (mapping == null)
            {
                mapping = new UserMovie { MovieId = id, UserId = user.Id, Seen = false };
                this.DatabaseContext.UserMovieSwitch.Add(mapping);
                this.DatabaseContext.SaveChanges();
                this.Logger.LogInformation(user, this.GetService(), this.GetEvent("add my"), id);
            }
        }

        public void RemoveMovieFromMyMovies(int id)
        {
            var user = this.Utils.GetCurrentUser();

            var mapping = this.DatabaseContext.UserMovieSwitch.Where(x => x.UserId == user.Id && x.MovieId == id).FirstOrDefault();

            if (mapping != null)
            {
                this.DatabaseContext.UserMovieSwitch.Remove(mapping);
                this.DatabaseContext.SaveChanges();
                this.Logger.LogInformation(user, this.GetService(), this.GetEvent("delete my"), id);
            }
        }
    }
}
