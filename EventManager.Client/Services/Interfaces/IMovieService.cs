using EventManager.Client.Http;
using ManagerAPI.Shared.DTOs.SL;
using ManagerAPI.Shared.Models.SL;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventManager.Client.Services.Interfaces
{
    public interface IMovieService : IHttpCall<MovieListDto, MovieDto, MovieModel>
    {
        Task<List<MyMovieListDto>> GetMyList();
        Task<MyMovieDto> GetMy(int id);
        Task<bool> UpdateSeenStatuses(List<MovieSeenUpdateModel> models);
        Task<bool> UpdateMyMovies(MyMovieModel model);
        Task<bool> AddMovieToMyMovies(int id);
        Task<bool> RemoveMovieFromMyMovies(int id);
        Task<List<MyMovieSelectorListDto>> GetMySelectorList(bool onlyMine);
        Task<bool> UpdateImage(int id, MovieImageModel model);
        Task<bool> UpdateCategories(int id, MovieCategoryUpdateModel model);
        Task<bool> UpdateRate(int id, MovieRateModel model);
    }
}