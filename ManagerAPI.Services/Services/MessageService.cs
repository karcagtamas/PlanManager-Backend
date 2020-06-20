using AutoMapper;
using ManagerAPI.DataAccess;
using ManagerAPI.Models.DTOs;
using ManagerAPI.Models.Entities;
using ManagerAPI.Models.Enums;
using ManagerAPI.Models.Models;
using ManagerAPI.Services.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ManagerAPI.Services.Services
{
    public class MessageService : IMessageService
    {
        private IUtilsService UtilsService { get; }
        private INotificationService NotificationService { get; }
        private DatabaseContext Context { get; }
        private IMapper Mapper { get; }

        public MessageService(IUtilsService utilsService, INotificationService notificationService, DatabaseContext context, IMapper mapper)
        {
            UtilsService = utilsService;
            NotificationService = notificationService;
            Context = context;
            Mapper = mapper;
        }


        public List<MessageDto> GetMessages(int friendId)
        {
            var user = UtilsService.GetCurrentUser();

            var list = Mapper.Map<List<MessageDto>>(user.SentMessages.Union(user.ReceivedMessages).OrderBy(x => x.Date).ToList()).Select(x => { x.IsMine = x.Sender == user.UserName; return x; }).ToList();

            UtilsService.LogInformation(MessageMessages.MyMessageGet, user);

            return list;
        }

        public void SendMessage(MessageModel model)
        {
            var user = UtilsService.GetCurrentUser();
            var partner = Context.AppUsers.Find(model.PartnerId);

            if (partner == null)
            {
                throw new Exception(MessageMessages.InvalidPartner);
            }

            var message = new Message();

            message.SenderId = user.Id;
            message.ReceiverId = model.PartnerId;
            message.Text = model.Message;

            Context.Messages.Add(message);
            Context.SaveChanges();

            UtilsService.LogInformation(MessageMessages.MessageSend, user);
            NotificationService.AddSystemNotificationByType(SystemNotificationType.MessageArrived, partner);
        }
    }
}
