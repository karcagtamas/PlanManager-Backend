using System.Collections.Generic;
using System.Threading.Tasks;
using EventManager.Client.Http;
using ManagerAPI.Shared.DTOs.MC;
using ManagerAPI.Shared.Models.MC;

namespace EventManager.Client.Services.Interfaces
{
    public interface IMovieService : IHttpCall<MovieListDto, MovieDto, MovieModel>
    {
        Task<List<MyMovieListDto>> GetMyList();
        Task<MyMovieDto> GetMy(int id);
        Task<bool> UpdateSeenStatus(int id, MovieSeenUpdateModel model);
        Task<bool> UpdateMyMovies(MyMovieModel model);
        Task<bool> AddMovieToMyMovies(int id);
        Task<bool> RemoveMovieFromMyMovies(int id);
    }
}