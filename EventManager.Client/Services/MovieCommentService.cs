using System.Collections.Generic;
using System.Threading.Tasks;
using EventManager.Client.Http;
using EventManager.Client.Models;
using EventManager.Client.Services.Interfaces;
using ManagerAPI.Shared.DTOs.SL;
using ManagerAPI.Shared.Models.SL;

namespace EventManager.Client.Services
{
    public class MovieCommentService : HttpCall<MovieCommentListDto, MovieCommentDto, MovieCommentModel>, IMovieCommentService
    {
        public MovieCommentService(IHttpService http) : base(http, $"{ApplicationSettings.BaseApiUrl}/movie-comment", "Movie Comment")
        {
        }

        public async Task<List<MovieCommentListDto>> GetList(int movieÍd)
        {
            var pathParams = new HttpPathParameters();
            pathParams.Add<int>(movieÍd, -1);

            var settings = new HttpSettings($"{this.Url}/movie", null, pathParams);

            return await this.Http.Get<List<MovieCommentListDto>>(settings);
        }
    }
}