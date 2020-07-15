using AutoMapper;
using ManagerAPI.DataAccess;
using ManagerAPI.Models.DTOs.MC;
using ManagerAPI.Models.Entities.MC;
using ManagerAPI.Services.Services.Interfaces;
using MovieCorner.Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MovieCorner.Services.Services
{
    public class MovieService : IMovieService
    {
        // Actions
        private const string UpdateMyMoviesAction = "update my movies";
        private const string SetMovieStatusAction = "set movie status";
        private const string DeleteMovieAction = "delete movie";
        private const string UpdateMovieAction = "update movie";
        private const string CreateMovieAction = "create movie";
        private const string GetMyMoviesAction = "get my movies";
        private const string GetMovieAction = "get movie";
        private const string GetMoviesAction = "get movies";

        // Things
        private const string MovieThing = "movie";
        private const string UserMovieThing = "user-movie";
        private const string MovieIdThing = "movie id";

        // Messages
        private const string UserMovieConnectionDoesNotExistMessage = "User Movie connection does not exist";
        private const string MovieDoesNotExistMessage = "Movie does not exist";
        private const string MovieIdsDoNotMatchMessage = "Movie ids do not match";

        // Injects
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        private readonly IUtilsService _utilsService;
        private readonly ILoggerService _loggerService;

        /// <summary>
        /// Injector Constructor
        /// </summary>
        /// <param name="context">Database Context</param>
        /// <param name="mapper">Mapper</param>
        /// <param name="utilsService">Utils Service</param>
        /// <param name="loggerService">Logger Service</param>
        public MovieService(DatabaseContext context, IMapper mapper, IUtilsService utilsService, ILoggerService loggerService)
        {
            _context = context;
            _mapper = mapper;
            _utilsService = utilsService;
            _loggerService = loggerService;
        }
        
        public List<MovieListDto> GetMovies()
        {
            var user = this._utilsService.GetCurrentUser();

            var list = this._context.Movies.ToList();

            this._loggerService.LogInformation(user, nameof(MovieService), GetMoviesAction, list.Select(x => x.Id).ToList());

            return this._mapper.Map<List<MovieListDto>>(list);
        }

        public MovieListDto GetMovie(int id)
        {
            var user = this._utilsService.GetCurrentUser();

            var movie = _context.Movies.Find(id);
            
            if (movie == null)
            {
                throw this._loggerService.LogInvalidThings(user, nameof(MovieService), MovieThing, MovieDoesNotExistMessage);
            }

            this._loggerService.LogInformation(user, nameof(MovieService), GetMovieAction, id);

            return _mapper.Map<MovieListDto>(movie);
        }

        public List<MovieDto> GetOwnMovies()
        {
            var user = this._utilsService.GetCurrentUser();

            var list = user.MyMovies.ToList();

            this._loggerService.LogInformation(user, nameof(MovieService), GetMyMoviesAction, list.Select(x => x.Movie.Id).ToList());

            return _mapper.Map<List<MovieDto>>(list);
        }

        public void CreateMovie(MovieCreateDto model)
        {
            var user = this._utilsService.GetCurrentUser();

            var movie = this._mapper.Map<Movie>(model);
            movie.CreatorId = user.Id;
            movie.LastUpdaterId = user.Id;

            _context.Movies.Add(movie);
            _context.SaveChanges();

            this._loggerService.LogInformation(user, nameof(MovieService), CreateMovieAction, movie.Id);
        }

        public void UpdateMovie(MovieUpdateDto model, int id)
        {
            var user = this._utilsService.GetCurrentUser();

            if (id != model.Id)
            {
                throw this._loggerService.LogInvalidThings(user, nameof(MovieService), MovieIdThing, MovieIdsDoNotMatchMessage);
            }

            var movie = _context.Movies.Find(model.Id);
            if (movie == null)
            {
                throw this._loggerService.LogInvalidThings(user, nameof(MovieService), MovieThing, MovieDoesNotExistMessage);
            }

            this._mapper.Map(model, movie);
            movie.LastUpdaterId = user.Id;
            movie.LastUpdate = DateTime.Now;

            _context.Movies.Update(movie);
            _context.SaveChanges();

            this._loggerService.LogInformation(user, nameof(MovieService), UpdateMovieAction, movie.Id);
        }

        public void DeleteMovie(int id)
        {
            var user = this._utilsService.GetCurrentUser();

            var movie = _context.Movies.Find(id);
            if (movie == null)
            {
                throw this._loggerService.LogInvalidThings(user, nameof(MovieService), MovieThing, MovieDoesNotExistMessage);
            }

            this._context.Movies.Remove(movie);
            this._context.SaveChanges();

            this._loggerService.LogInformation(user, nameof(MovieService), DeleteMovieAction, movie.Id);
        }

        public void UpdateSeenStatus(int id, bool seen)
        {
            var user = this._utilsService.GetCurrentUser();

            var userMovie = _context.UserMovieSwitch.Find(user.Id, id);
            if (userMovie == null)
            {
                throw this._loggerService.LogInvalidThings(user, nameof(MovieService), UserMovieThing, UserMovieConnectionDoesNotExistMessage);
            }

            userMovie.Seen = seen;
            _context.UserMovieSwitch.Update(userMovie);
            _context.SaveChanges();

            this._loggerService.LogInformation(user, nameof(MovieService), SetMovieStatusAction, userMovie.Movie.Id);
        }


        public void UpdateMovieMappings(List<int> mappings)
        {
            var user = this._utilsService.GetCurrentUser();

            var currentMappings = _context.UserMovieSwitch.Where(x => x.UserId == user.Id).ToList();
            foreach (var i in currentMappings)
            {
                if (mappings.FindIndex(x => x == i.MovieId) == -1)
                {
                    _context.UserMovieSwitch.Remove(i);
                }
            }

            foreach (var i in mappings)
            {
                if (currentMappings.FirstOrDefault(x => x.MovieId == i) == null)
                {
                    var map = new UserMovie() { MovieId = i, UserId = user.Id, Seen = false };
                    _context.UserMovieSwitch.Add(map);
                }
            }

            this._loggerService.LogInformation(user, nameof(MovieService), UpdateMyMoviesAction, mappings);
            _context.SaveChanges();
        }
    }
}
