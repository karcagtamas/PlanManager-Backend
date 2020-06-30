using System.Net.Http;
using System.Threading.Tasks;
using EventManager.Client.Models;

namespace EventManager.Client.Services {
    public class HttpService : IHttpService {
        private readonly HttpClient _httpClient;
        private readonly IHelperService _helperService;

        public HttpService (HttpClient httpClient, IHelperService helperService) {
            this._httpClient = httpClient;
            this._helperService = helperService;
        }

        public async Task<bool> create<T> (HttpSettings settings, HttpBody<T> body) {
            var response = await _httpClient.PostAsync (CreateUrl (settings), body.GetStringContent ());

            if (settings.ToasterSettings.IsNeeded) {
                await _helperService.AddToaster (response, settings.ToasterSettings.Caption);
            }

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> delete (HttpSettings settings) {
            var response = await _httpClient.DeleteAsync (CreateUrl (settings));

            if (settings.ToasterSettings.IsNeeded) {
                await _helperService.AddToaster (response, settings.ToasterSettings.Caption);
            }

            return response.IsSuccessStatusCode;
        }

        public async Task<T> get<T> (HttpSettings settings) {
            var response = await _httpClient.GetAsync (CreateUrl (settings));

            if (response.IsSuccessStatusCode) {
                using (var sr = await response.Content.ReadAsStreamAsync ()) {
                    return await System.Text.Json.JsonSerializer.DeserializeAsync<T> (sr, _helperService.GetSerializerOptions ());
                }
            } else {
                return default;
            }
        }

        public async Task<bool> update<T> (HttpSettings settings, HttpBody<T> body) {
            var response = await _httpClient.PutAsync (CreateUrl (settings), body.GetStringContent ());

            if (settings.ToasterSettings.IsNeeded) {
                await _helperService.AddToaster (response, settings.ToasterSettings.Caption);
            }

            return response.IsSuccessStatusCode;
        }

        private string CreateUrl (HttpSettings settings) {
            string url = settings.Url;

            if (settings.PathParameters.Count () > 0) {
                url += settings.PathParameters.ToString ();
            }

            if (settings.QueryParameters.Count () > 0) {
                url += $"?{settings.QueryParameters.ToString()}";
            }

            return url;
        }
    }
}