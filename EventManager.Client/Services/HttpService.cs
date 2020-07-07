using System;
using System.Net.Http;
using System.Threading.Tasks;
using EventManager.Client.Models;
using EventManager.Client.Services.Interfaces;

/// <summary>
/// HTTP Service
/// </summary>
namespace EventManager.Client.Services {
    public class HttpService : IHttpService {
        private readonly HttpClient _httpClient;
        private readonly IHelperService _helperService;

        /// <summary>
        /// HTTP Service Injector
        /// </summary>
        /// <param name="httpClient">HTTP Client</param>
        /// <param name="helperService">Helper Service</param>
        public HttpService (HttpClient httpClient, IHelperService helperService) {
            this._httpClient = httpClient;
            this._helperService = helperService;
        }

        /// <summary>
        /// POST request
        /// </summary>
        /// <param name="settings">Http settings</param>
        /// <param name="body">Body of post request</param>
        /// <typeparam name="T">Type of the body</typeparam>
        /// <returns>The request was success or not</returns>
        public async Task<bool> create<T> (HttpSettings settings, HttpBody<T> body) 
        {
            this.CheckSettings(settings);

            var response = await _httpClient.PostAsync (CreateUrl (settings), body.GetStringContent ());

            // Optional toast
            if (settings.ToasterSettings.IsNeeded) {
                await _helperService.AddToaster (response, settings.ToasterSettings.Caption);
            }

            return response.IsSuccessStatusCode;
        }

        /// <summary>
        /// POST request where we want string response
        /// </summary>
        /// <param name="settings">Http settings</param>
        /// <param name="body">Body of post request</param>
        /// <typeparam name="T">Type of the body</typeparam>
        /// <returns>Response string value</returns>
        public async Task<string> createString<T>(HttpSettings settings, HttpBody<T> body)
        {
            this.CheckSettings(settings);

            var response = await _httpClient.PostAsync (CreateUrl (settings), body.GetStringContent());

            // Optional toast
            if (settings.ToasterSettings.IsNeeded) {
                await _helperService.AddToaster (response, settings.ToasterSettings.Caption);
            }

            // Deserialize json
            if (response.IsSuccessStatusCode) {
                return await response.Content.ReadAsStringAsync ();
            } else {
                return "";
            }
        }

        /// <summary>
        /// DELETE request
        /// </summary>
        /// <param name="settings">Http settings</param>
        /// <returns>The request was success or not</returns>
        public async Task<bool> delete (HttpSettings settings) 
        {
            this.CheckSettings(settings);

            var response = await _httpClient.DeleteAsync (CreateUrl (settings));

            // Optional toast
            if (settings.ToasterSettings.IsNeeded) {
                await _helperService.AddToaster (response, settings.ToasterSettings.Caption);
            }

            return response.IsSuccessStatusCode;
        }

        /// <summary>
        /// GET request
        /// </summary>
        /// <param name="settings">Http settings</param>
        /// <typeparam name="T">Type of the result</typeparam>
        /// <returns>Response as T type</returns>
        public async Task<T> get<T> (HttpSettings settings) 
        {
            this.CheckSettings(settings);
            
            var response = await _httpClient.GetAsync (CreateUrl (settings));

            // Deserialize json
            if (response.IsSuccessStatusCode) {
                using (var sr = await response.Content.ReadAsStreamAsync ()) {
                    return await System.Text.Json.JsonSerializer.DeserializeAsync<T> (sr, _helperService.GetSerializerOptions ());
                }
            } else {
                return default;
            }
        }

        /// <summary>
        /// Get number
        /// </summary>
        /// <param name="settings">Http settings</param>
        /// <returns>Number response</returns>
        public async Task<int?> getInt(HttpSettings settings)
        {
            this.CheckSettings(settings);

            var response = await _httpClient.GetAsync (CreateUrl (settings));

            if (response.IsSuccessStatusCode) {
                int count = -1;
                int.TryParse (await response.Content.ReadAsStringAsync (), out count);
                return count == -1 ? null : (int?) count;
            } else {
                return null;
            }
        }

        /// <summary>
        /// Get string
        /// </summary>
        /// <param name="settings">Http settings</param>
        /// <returns>String response</returns>
        public async Task<string> getString(HttpSettings settings)
        {
            this.CheckSettings(settings);

            var response = await _httpClient.GetAsync (CreateUrl (settings));

            // Deserialize json
            if (response.IsSuccessStatusCode) {
                return await response.Content.ReadAsStringAsync ();
            } else {
                return default;
            }
        }

        /// <summary>
        /// PUT request
        /// </summary>
        /// <param name="settings">Http settings</param>
        /// <param name="body">Body of put request</param>
        /// <typeparam name="T">Type of the body</typeparam>
        /// <returns>The request was success or not</returns>
        public async Task<bool> update<T> (HttpSettings settings, HttpBody<T> body) {
            this.CheckSettings(settings);

            var response = await _httpClient.PutAsync (CreateUrl (settings), body.GetStringContent ());

            // Optional toast
            if (settings.ToasterSettings.IsNeeded) {
                await _helperService.AddToaster (response, settings.ToasterSettings.Caption);
            }

            return response.IsSuccessStatusCode;
        }

        /// <summary>
        /// Create url from http settings
        /// Concat url, path parameters and query parameters
        /// </summary>
        /// <param name="settings">Http settings</param>
        /// <returns>Created url</returns>
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

        private void CheckSettings(HttpSettings settings) {
            if (settings == null) {
                throw new ArgumentException("Settings cannot be null");
            }
        }
    }
}