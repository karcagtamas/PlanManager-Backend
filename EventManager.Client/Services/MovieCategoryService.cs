using EventManager.Client.Http;
using EventManager.Client.Models;
using EventManager.Client.Services.Interfaces;
using ManagerAPI.Shared.DTOs.SL;
using ManagerAPI.Shared.Models.SL;

namespace EventManager.Client.Services
{
    public class MovieCategoryService : HttpCall<MovieCategoryDto, MovieCategoryDto, MovieCategoryModel>, IMovieCategoryService
    {
        protected MovieCategoryService(IHttpService http) : base(http, $"{ApplicationSettings.BaseApiUrl}/movie-category", "Movie Category")
        {
        }
    }
}