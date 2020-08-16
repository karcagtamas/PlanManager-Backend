using System.Collections.Generic;
using System.Threading.Tasks;
using EventManager.Client.Http;
using ManagerAPI.Shared.DTOs.SL;
using ManagerAPI.Shared.Models.SL;

namespace EventManager.Client.Services.Interfaces
{
    public interface IMovieCategoryService : IHttpCall<MovieCategoryDto, MovieCategoryDto, MovieCategoryModel>
    {
        Task<List<MovieCategorySelectorListDto>> GetSelectorList(int movieId);
    }
}