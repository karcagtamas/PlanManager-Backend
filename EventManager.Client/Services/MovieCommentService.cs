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
    }
}