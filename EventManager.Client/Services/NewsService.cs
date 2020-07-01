using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using EventManager.Client.Models;
using EventManager.Client.Models.News;
using EventManager.Client.Services.Interfaces;

namespace EventManager.Client.Services {
    public class NewsService : INewsService {
        private readonly HttpClient _httpClient;
        private readonly string _url = ApplicationSettings.BaseApiUrl + "/news";
        private readonly IHelperService _helperService;

        public NewsService (HttpClient httpClient, IHelperService helperService) {
            _httpClient = httpClient;
            _helperService = helperService;
        }

        public async Task<bool> DeleteNews (int postId) {
            var response = await _httpClient.DeleteAsync ($"{_url}/{postId}");

            await _helperService.AddToaster (response, "News deleting");

            return response.IsSuccessStatusCode;
        }

        public async Task<List<NewsDto>> GetNewsPosts () {
            var response = await _httpClient.GetAsync ($"{_url}");

            if (response.IsSuccessStatusCode) {
                using (var sr = await response.Content.ReadAsStreamAsync ()) {
                    return await System.Text.Json.JsonSerializer.DeserializeAsync<List<NewsDto>> (sr, _helperService.GetSerializerOptions ());
                }
            } else {
                return null;
            }
        }

        public async Task<bool> PostNews (PostModel model) {
            var response = await _httpClient.PostAsync ($"{_url}", _helperService.CreateContent (model));

            await _helperService.AddToaster (response, "News creating");

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateNews (int postId, PostModel model) {
            var response = await _httpClient.PostAsync ($"{_url}/{postId}", _helperService.CreateContent (model));

            await _helperService.AddToaster (response, "News updating");

            return response.IsSuccessStatusCode;
        }
    }
}