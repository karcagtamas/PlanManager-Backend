using System.Collections.Generic;
using ManagerAPI.Models.DTOs.MC;

namespace MovieCorner.Services.Services.Interfaces
{
    public interface IMovieService
    {
        List<MovieListDto> GetMovies();

        MovieListDto GetMovie(int id);

        List<MovieDto> GetOwnMovies();

        void CreateMovie(MovieCreateDto model);

        void UpdateMovie(MovieUpdateDto model, int id);

        void DeleteMovie(int id);

        void UpdateSeenStatus(int id, bool seen);

        void UpdateMovieMappings(List<int> mappings);
    }
}
