using EventManager.Client.Http;
using EventManager.Client.Models;
using EventManager.Client.Services.Interfaces;
using ManagerAPI.Shared.DTOs;
using ManagerAPI.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventManager.Client.Services
{
    public class MessageService : HttpCall<MessageListDto, MessageDto, MessageModel>, IMessageService
    {
        private readonly IHelperService _helperService;

        public MessageService(IHttpService httpService, IHelperService helperService) : base(httpService, $"{ApplicationSettings.BaseApiUrl}/message", "Message")
        {
            this._helperService = helperService;
        }

        public async Task<List<MessageDto>> GetMessages(int friendId)
        {
            var pathParams = new HttpPathParameters();
            pathParams.Add<int>(friendId, -1);

            var settings = new HttpSettings($"{this.Url}/friend", null, pathParams);

            return await this.Http.Get<List<MessageDto>>(settings);
        }

        public async Task<bool> SendMessage(MessageModel model)
        {
            var settings = new HttpSettings($"{this.Url}/send", null, null, "Message sending");

            var body = new HttpBody<MessageModel>(model);

            return await this.Http.Create<MessageModel>(settings, body);
        }
    }
}