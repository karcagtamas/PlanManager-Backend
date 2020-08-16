using EventManager.Client.Http;
using ManagerAPI.Shared.DTOs.SL;
using ManagerAPI.Shared.Models.SL;

namespace EventManager.Client.Services.Interfaces
{
    public interface IMovieCategoryService : IHttpCall<MovieCategoryDto, MovieCategoryDto, MovieCategoryModel>
    {
        
    }
}