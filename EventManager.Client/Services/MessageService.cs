using System.Collections.Generic;
using System.Threading.Tasks;
using EventManager.Client.Http;
using EventManager.Client.Models;
using EventManager.Client.Services.Interfaces;
using ManagerAPI.Shared.DTOs;
using ManagerAPI.Shared.Models;

namespace EventManager.Client.Services
{
    public class MessageService : HttpCall<MessageListDto, MessageDto, MessageModel>, IMessageService
    {

        private readonly IHttpService _httpService;
        private readonly string _url = ApplicationSettings.BaseApiUrl + "/message";
        private readonly IHelperService _helperService;

        public MessageService(IHttpService httpService, IHelperService helperService) : base(httpService, $"{ApplicationSettings.BaseApiUrl}/message", "Message")
        {
            _httpService = httpService;
            _helperService = helperService;
        }

        public async Task<List<MessageDto>> GetMessages(int friendId)
        {
            var pathParams = new HttpPathParameters();
            pathParams.Add<int>(friendId, -1);

            var settings = new HttpSettings($"{this._url}/friend", null, pathParams);

            return await this._httpService.Get<List<MessageDto>>(settings);
        }

        public async Task<bool> SendMessage(MessageModel model)
        {
            var settings = new HttpSettings($"{this._url}/send", null, null, "Message sending");

            var body = new HttpBody<MessageModel>(model);

            return await this._httpService.Create<MessageModel>(settings, body);
        }
    }
}