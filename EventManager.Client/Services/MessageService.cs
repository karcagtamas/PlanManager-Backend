using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using EventManager.Client.Models;
using EventManager.Client.Models.Messages;

namespace EventManager.Client.Services
{
    public class MessageService : IMessageService
    {

        private readonly HttpClient _httpClient;
        private readonly string _url = ApplicationSettings.BaseApiUrl + "/user";
        private readonly IHelperService _helperService;

        public MessageService(HttpClient httpClient, IHelperService helperService)
        {
            _httpClient = httpClient;
            _helperService = helperService;
        }
        
        public async Task<List<MessageDto>> GetMessages(int friendId)
        {
            var response = await _httpClient.GetAsync($"{_url}");

            if (response.IsSuccessStatusCode)
            {
                using (var sr = await response.Content.ReadAsStreamAsync()) 
                {
                    return await System.Text.Json.JsonSerializer.DeserializeAsync<List<MessageDto>>(sr, _helperService.GetSerializerOptions());
                }
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> SendMessage(MessageModel model)
        {
            var response = await _httpClient.PutAsync($"{_url}", _helperService.CreateContent(model));

            await _helperService.AddToaster(response, "Message sending");

            return response.IsSuccessStatusCode;
        }
    }
}