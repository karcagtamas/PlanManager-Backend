using EventManager.Client.Http;
using ManagerAPI.Shared.DTOs.SL;
using ManagerAPI.Shared.Models.SL;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventManager.Client.Services.Interfaces
{
    public interface IMovieCategoryService : IHttpCall<MovieCategoryDto, MovieCategoryDto, MovieCategoryModel>
    {
        Task<List<MovieCategorySelectorListDto>> GetSelectorList(int movieId);
    }
}