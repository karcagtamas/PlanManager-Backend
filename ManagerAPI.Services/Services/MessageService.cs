using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ManagerAPI.DataAccess;
using ManagerAPI.Domain.Entities;
using ManagerAPI.Domain.Enums;
using ManagerAPI.Services.Common.Repository;
using ManagerAPI.Services.Services.Interfaces;
using ManagerAPI.Shared.DTOs;
using ManagerAPI.Shared.Models;

namespace ManagerAPI.Services.Services
{
    /// <summary>
    /// Message Service
    /// </summary>
    public class MessageService : Repository<Message, SystemNotificationType>, IMessageService
    {
        // Actions
        private const string SendMessageAction = "send message";
        private const string GetMessagesAction = "get messages";

        // Things
        private const string MessageThing = "message";

        // Messages
        private const string ParnterIsNeededForSendingMessage = "Partner is needed for the message sending";

        // Injects
        private readonly DatabaseContext _databaseContext;

        /// <summary>
        /// Injector Constructor
        /// </summary>
        /// <param name="utilsService">Utils Service</param>
        /// <param name="notificationService">Notification Service</param>
        /// <param name="context">Database Context</param>
        /// <param name="mapper">Mapper</param>
        /// <param name="loggerService">Logger Service</param>
        public MessageService(IUtilsService utilsService, INotificationService notificationService, DatabaseContext context, IMapper mapper, ILoggerService loggerService) : base(context, loggerService, utilsService, notificationService, mapper, "Message", new NotificationArguments { DeleteArguments = new List<string>(), UpdateArguments = new List<string>(), CreateArguments = new List<string>() })
        {
            this._databaseContext = context;
        }

        /// <summary>
        /// Get current user's messages
        /// </summary>
        /// <param name="friendId">Partner Id</param>
        /// <returns>List of messages</returns>
        public List<MessageDto> GetMessages(string friendId)
        {
            var user = this.Utils.GetCurrentUser();

            var list = this.Mapper.Map<List<MessageDto>>(user.SentMessages.Where(x => x.Receiver.Id == friendId).Union(user.ReceivedMessages.Where(x => x.Sender.Id == friendId)).OrderBy(x => x.Date).ToList()).Select(x => { x.IsMine = x.Sender == user.UserName; return x; }).ToList();

            this.Logger.LogInformation(user, nameof(MessageService), GetMessagesAction, list.Select(x => x.Id).ToList());

            return list;
        }

        /// <summary>
        /// Send message to partner
        /// </summary>
        /// <param name="model">Model of message sending</param>
        public void SendMessage(MessageModel model)
        {
            var user = this.Utils.GetCurrentUser();
            var partner = this._databaseContext.AppUsers.Find(model.PartnerId);

            if (partner == null)
            {
                throw this.Logger.LogInvalidThings(user, nameof(MessageService), MessageThing, ParnterIsNeededForSendingMessage);
            }

            var message = new Message();

            message.SenderId = user.Id;
            message.ReceiverId = model.PartnerId;
            message.Text = model.Message;

            this._databaseContext.Messages.Add(message);
            this._databaseContext.SaveChanges();

            this.Logger.LogInformation(user, nameof(MessageService), SendMessageAction, $"{user.Id}-{partner.Id}");
            this.Notification.AddSystemNotificationByType(SystemNotificationType.MessageArrived, partner, user.UserName);
        }
    }
}
