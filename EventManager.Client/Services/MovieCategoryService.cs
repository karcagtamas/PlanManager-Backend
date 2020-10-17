using EventManager.Client.Http;
using EventManager.Client.Models;
using EventManager.Client.Services.Interfaces;
using ManagerAPI.Shared.DTOs.SL;
using ManagerAPI.Shared.Models.SL;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventManager.Client.Services
{
    public class MovieCategoryService : HttpCall<MovieCategoryDto, MovieCategoryDto, MovieCategoryModel>, IMovieCategoryService
    {
        public MovieCategoryService(IHttpService http) : base(http, $"{ApplicationSettings.BaseApiUrl}/movie-category", "Movie Category")
        {
        }

        public async Task<List<MovieCategorySelectorListDto>> GetSelectorList(int movieId)
        {
            var pathParams = new HttpPathParameters();
            pathParams.Add<int>(movieId, -1);

            var settings = new HttpSettings($"{this.Url}/selector", null, pathParams);

            return await this.Http.Get<List<MovieCategorySelectorListDto>>(settings);
        }
    }
}