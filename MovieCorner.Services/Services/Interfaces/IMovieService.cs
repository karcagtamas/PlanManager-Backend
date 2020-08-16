using System.Collections.Generic;
using ManagerAPI.Domain.Entities.SL;
using ManagerAPI.Services.Common.Repository;
using ManagerAPI.Shared.DTOs.SL;
using ManagerAPI.Shared.Models.SL;

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
        void UpdateImage(int id, MovieImageModel model);
        void UpdateCategories(int id, MovieCategoryUpdateModel model);
    }
}
