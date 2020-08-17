using System.Collections.Generic;
using System.Threading.Tasks;
using EventManager.Client.Http;
using EventManager.Client.Models;
using EventManager.Client.Services.Interfaces;
using ManagerAPI.Shared.DTOs.SL;
using ManagerAPI.Shared.Models.SL;

namespace EventManager.Client.Services
{
    public class MovieService : HttpCall<MovieListDto, MovieDto, MovieModel>, IMovieService
    {
        private readonly IHelperService _helperService;

        public MovieService(IHelperService helperService, IHttpService httpService) : base(httpService, $"{ApplicationSettings.BaseApiUrl}/movie", "Movies")
        {
            this._helperService = helperService;
        }

        public async Task<bool> AddMovieToMyMovies(int id)
        {
            var pathParams = new HttpPathParameters();
            pathParams.Add<int>(id, -1);
            var settings = new HttpSettings($"{this.Url}/map", null, pathParams, "Adding movie to My Movies");

            var body = new HttpBody<object>(null);

            return await this.Http.Create<object>(settings, body);
        }

        public async Task<List<MyMovieListDto>> GetMyList()
        {
            var settings = new HttpSettings($"{this.Url}/my");

            return await this.Http.Get<List<MyMovieListDto>>(settings);
        }
        
        public async Task<MyMovieDto> GetMy(int id)
        {
            var pathParams = new HttpPathParameters();
            pathParams.Add<int>(id, -1);
            
            var settings = new HttpSettings($"{this.Url}/my", null, pathParams);

            return await this.Http.Get<MyMovieDto>(settings);
        }

        public async Task<bool> RemoveMovieFromMyMovies(int id)
        {
            var pathParams = new HttpPathParameters();
            pathParams.Add<int>(id, -1);
            var settings = new HttpSettings($"{this.Url}/map", null, pathParams, "Removing movie from My Movies");

            return await this.Http.Delete(settings);
        }

        public async Task<List<MyMovieSelectorListDto>> GetMySelectorList(bool onlyMine)
        {
            var queryParams = new HttpQueryParameters();
            queryParams.Add("onlyMine", onlyMine);
            
            var settings = new HttpSettings($"{this.Url}/selector", queryParams, null);
            
            return await this.Http.Get<List<MyMovieSelectorListDto>>(settings);
        }

        public async Task<bool> UpdateImage(int id, MovieImageModel model)
        {
            var pathParams = new HttpPathParameters();
            pathParams.Add<int>(id, -1);
            
            var settings = new HttpSettings($"{this.Url}/image", null, pathParams, "Movie image updating");
            
            var body = new HttpBody<MovieImageModel>(model);

            return await this.Http.Update<MovieImageModel>(settings, body);
        }

        public async Task<bool> UpdateCategories(int id, MovieCategoryUpdateModel model)
        {
            var pathParams = new HttpPathParameters();
            pathParams.Add<int>(id, -1);
            
            var settings = new HttpSettings($"{this.Url}/categories", null, pathParams, "Movie category updating");
            
            var body = new HttpBody<MovieCategoryUpdateModel>(model);

            return await this.Http.Update<MovieCategoryUpdateModel>(settings, body);
        }

        public async Task<bool> UpdateRate(int id, MovieRateModel model)
        {
            var pathParams = new HttpPathParameters();
            pathParams.Add<int>(id, -1);
            
            var settings = new HttpSettings($"{this.Url}/rate", null, pathParams, "Movie rating");
            
            var body = new HttpBody<MovieRateModel>(model);

            return await this.Http.Update<MovieRateModel>(settings, body);
        }

        public async Task<bool> UpdateMyMovies(MyMovieModel model)
        {
            var settings = new HttpSettings($"{this.Url}/map", null, null, "My Movies updating");

            var body = new HttpBody<MyMovieModel>(model);

            return await this.Http.Update<MyMovieModel>(settings, body);
        }

        public async Task<bool> UpdateSeenStatuses(List<MovieSeenUpdateModel> models)
        {
            var settings = new HttpSettings($"{this.Url}/map/status", null, null, "My Movie seen status updating");

            var body = new HttpBody<List<MovieSeenUpdateModel>>(models);

            return await this.Http.Update<List<MovieSeenUpdateModel>>(settings, body);
        }
    }
}