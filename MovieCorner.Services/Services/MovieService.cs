using AutoMapper;
using ManagerAPI.DataAccess;
using ManagerAPI.Domain.Entities.MC;
using ManagerAPI.Services.Common;
using ManagerAPI.Services.Services.Interfaces;
using ManagerAPI.Shared.DTOs.MC;
using ManagerAPI.Shared.Models.MC;
using MovieCorner.Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MovieCorner.Services.Services
{
    public class MovieService : Repository<Movie>, IMovieService
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
        public MovieService(DatabaseContext context, IMapper mapper, IUtilsService utilsService, ILoggerService loggerService) : base(context, loggerService, utilsService, mapper, "Movie")
        {
            this.DatabaseContext = context;
        }

        public List<MyMovieDto> GetMyMovies()
        {
            var user = this.Utils.GetCurrentUser();

            var list = user.MyMovies.ToList();

            this.Logger.LogInformation(user, this.GetService(), this.GetEvent("get my"), list.Select(x => x.Movie.Id).ToList());

            return Mapper.Map<List<MyMovieDto>>(list);
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
    }
}
