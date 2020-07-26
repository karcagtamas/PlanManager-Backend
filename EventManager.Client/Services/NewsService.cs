using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using EventManager.Client.Models;
using EventManager.Client.Services.Interfaces;
using ManagerAPI.Shared.DTOs;
using ManagerAPI.Shared.Models;

namespace EventManager.Client.Services
{
    public class NewsService : INewsService
    {
        private readonly IHttpService _httpService;
        private readonly string _url = ApplicationSettings.BaseApiUrl + "/news";
        private readonly IHelperService _helperService;

        public NewsService(IHttpService httpService, IHelperService helperService)
        {
            _httpService = httpService;
            _helperService = helperService;
        }

        public async Task<bool> DeleteNews(int postId)
        {
            var pathParams = new HttpPathParameters();
            pathParams.Add<int>(postId, -1);

            var settings = new HttpSettings($"{this._url}", null, pathParams, "News deleting");

            return await this._httpService.Delete(settings);
        }

        public async Task<List<NewsDto>> GetNewsPosts()
        {
            var settings = new HttpSettings($"{this._url}");

            return await this._httpService.Get<List<NewsDto>>(settings);
        }

        public async Task<bool> PostNews(PostModel model)
        {
            var settings = new HttpSettings($"{this._url}", null, null, "News creating");

            var body = new HttpBody<PostModel>(model);

            return await this._httpService.Create<PostModel>(settings, body);
        }

        public async Task<bool> UpdateNews(int postId, PostModel model)
        {
            var pathParams = new HttpPathParameters();
            pathParams.Add<int>(postId, -1);

            var settings = new HttpSettings($"{this._url}", null, pathParams, "News updating");

            var body = new HttpBody<PostModel>(model);

            return await this._httpService.Update<PostModel>(settings, body);
        }
    }
}