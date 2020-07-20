using System.Collections.Generic;
using System.Threading.Tasks;
using EventManager.Client.Models.SL;

namespace EventManager.Client.Services.Interfaces
{
    public interface IMovieService
    {
        Task<List<MovieListDto>> GetMovies();
        Task<MovieDto> GetMovie(int id);
        Task<List<MyMovieDto>> GetMyMovies();
        Task<bool> CreateMovie(MovieModel model);
        Task<bool> UpdateMovie(int id, MovieModel model);
        Task<bool> DeleteMovie(int id);
        Task<bool> UpdateSeenStatus(int id, bool seen);
        Task<bool> UpdateMyMovies(List<int> ids);
    }
}