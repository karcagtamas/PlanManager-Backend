using System.Collections.Generic;
using System.Threading.Tasks;
using EventManager.Client.Http;
using EventManager.Client.Models;
using EventManager.Client.Services.Interfaces;
using ManagerAPI.Shared.DTOs.MC;
using ManagerAPI.Shared.Models.MC;

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

        public async Task<List<MyMovieDto>> GetMyMovies()
        {
            var settings = new HttpSettings($"{this.Url}");

            return await this.Http.Get<List<MyMovieDto>>(settings);
        }

        public async Task<bool> RemoveMovieFromMyMovies(int id)
        {
            var pathParams = new HttpPathParameters();
            pathParams.Add<int>(id, -1);
            var settings = new HttpSettings($"{this.Url}/map", null, pathParams, "Removing movie from My Movies");

            return await this.Http.Delete(settings);
        }

        public async Task<bool> UpdateMyMovies(MyMovieModel model)
        {
            var settings = new HttpSettings($"{this.Url}", null, null, "My Movies updating");

            var body = new HttpBody<MyMovieModel>(model);

            return await this.Http.Update<MyMovieModel>(settings, body);
        }

        public async Task<bool> UpdateSeenStatus(int id, MovieSeenUpdateModel model)
        {
            var settings = new HttpSettings($"{this.Url}", null, null, "My Movie seen status updating");

            var body = new HttpBody<MovieSeenUpdateModel>(model);

            return await this.Http.Update<MovieSeenUpdateModel>(settings, body);
        }
    }
}