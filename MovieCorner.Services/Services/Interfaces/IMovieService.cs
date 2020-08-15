using System.Collections.Generic;
using ManagerAPI.Domain.Entities.MC;
using ManagerAPI.Services.Common.Repository;
using ManagerAPI.Shared.DTOs.MC;

namespace MovieCorner.Services.Services.Interfaces
{
    public interface IMovieService : IRepository<Movie>
    {
        List<MyMovieListDto> GetMyList();
        MyMovieDto GetMy(int id);
        void UpdateSeenStatus(int id, bool seen);
        void UpdateMyMovies(List<int> ids);
        void AddMovieToMyMovies(int id);
        void RemoveMovieFromMyMovies(int id);
        List<MyMovieSelectorListDto> GetMySelectorList(bool onlyMine);
    }
}
