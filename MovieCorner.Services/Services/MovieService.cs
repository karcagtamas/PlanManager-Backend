using AutoMapper;
using ManagerAPI.DataAccess;

namespace MovieCorner.Services.Services
{
    public class MovieService
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        public MovieService(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        /*
        public List<MovieListDTO> GetMovies()
        {
            return _context.Movies.Select(x => _mapper.Map<MovieListDTO>(x)).ToList();
        }

        public MovieListDTO GetMovie(int id)
        {
            var movie = _context.Movies.Find(id);
            if (movie == null)
            {
                throw new Exception("Movie does not exist");
            }
            return _mapper.Map<MovieListDTO>(movie);
        }

        public List<MovieDTO> GetOwnMovies(string userId)
        {
            return _context.UserMovie.Where(x => x.UserId == userId).Select(x => _mapper.Map<MovieDTO>(x)).ToList();
        }

        public MovieListDTO CreateMovie(MovieCreateDTO model, string userId)
        {
            if (model == null)
            {
                throw new Exception("Invalid create form");
            }
            try
            {
                var movie = _mapper.Map<Movie>(model);
                movie.CreaterId = userId;
                _context.Movies.Add(movie);
                _context.SaveChanges();
                return _mapper.Map<MovieListDTO>(movie);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void UpdateMovie(MovieUpdateDTO model, int id)
        {
            if (id != model.Id)
            {
                throw new Exception("Invalid update");
            }
            var movie = _context.Movies.Find(model.Id);
            if (movie == null)
            {
                throw new Exception("Movie does not exist");
            }
            movie.Title = model.Title;
            movie.Description = model.Description;
            movie.Year = model.Year;

            try
            {
                _context.Movies.Update(movie);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void DeleteMovie(int id)
        {
            var movie = _context.Movies.Find(id);
            if (movie == null)
            {
                throw new Exception("Movie does not exist");
            }
            try
            {

                _context.Movies.Remove(movie);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void UpdateSeenStatus(int id, string userId, bool seen)
        {
            var userMovie = _context.UserMovie.Find(id, userId);
            if (userMovie == null)
            {
                throw new Exception("User and movie connection does not exist");
            }
            try
            {
                userMovie.Seen = seen;
                _context.UserMovie.Update(userMovie);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public void UpdateMovieMappings(string userId, List<MovieListDTO> mappings)
        {
            var currentMappings = _context.UserMovie.Where(x => x.UserId == userId).ToList();
            foreach (var i in currentMappings)
            {
                if (mappings.FindIndex(x => x.Id == i.MovieId) == -1)
                {
                    _context.UserMovie.Remove(i);
                }
            }

            foreach (var i in mappings)
            {
                if (currentMappings.FirstOrDefault(x => x.MovieId == i.Id) == null)
                {
                    var map = new UserMovie() { MovieId = i.Id, UserId = userId, Seen = false };
                    _context.UserMovie.Add(map);
                }
            }
            _context.SaveChanges();
        }
        
        */
    }
}
