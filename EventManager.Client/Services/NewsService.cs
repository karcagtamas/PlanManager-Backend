using EventManager.Client.Http;
using EventManager.Client.Models;
using EventManager.Client.Services.Interfaces;
using ManagerAPI.Shared.DTOs;
using ManagerAPI.Shared.Models;

namespace EventManager.Client.Services
{
    public class NewsService : HttpCall<NewsListDto, NewsDto, PostModel>, INewsService
    {

        public NewsService(IHttpService httpService) : base(httpService, $"{ApplicationSettings.BaseApiUrl}/news", "News")
        {
        }
    }
}