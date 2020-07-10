using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using EventManager.Client.Models;
using EventManager.Client.Models.Messages;
using EventManager.Client.Services.Interfaces;

namespace EventManager.Client.Services
{
    public class MessageService : IMessageService
    {

        private readonly IHttpService _httpService;
        private readonly string _url = ApplicationSettings.BaseApiUrl + "/user";
        private readonly IHelperService _helperService;

        public MessageService(IHttpService httpService, IHelperService helperService)
        {
            _httpService = httpService;
            _helperService = helperService;
        }

        public async Task<List<MessageDto>> GetMessages(int friendId)
        {
            var pathParams = new HttpPathParameters();
            pathParams.Add<int>(friendId, -1);

            var settings = new HttpSettings($"{this._url}", null, pathParams);

            return await this._httpService.get<List<MessageDto>>(settings);
        }

        public async Task<bool> SendMessage(MessageModel model)
        {
            var settings = new HttpSettings($"{this._url}", null, null, "Message sending");

            var body = new HttpBody<MessageModel>(this._helperService, model);

            return await this._httpService.create<MessageModel>(settings, body);
        }
    }
}