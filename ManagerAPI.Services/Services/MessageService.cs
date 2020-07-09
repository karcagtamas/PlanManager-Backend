using AutoMapper;
using ManagerAPI.DataAccess;
using ManagerAPI.Models.DTOs;
using ManagerAPI.Models.Entities;
using ManagerAPI.Models.Enums;
using ManagerAPI.Models.Models;
using ManagerAPI.Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ManagerAPI.Services.Services
{
    /// <summary>
    /// Message Service
    /// </summary>
    public class MessageService : IMessageService
    {
        // Actions
        private const string SendMessageAction = "send message";
        private const string GetMessagesAction = "get messages";
        
        // Things
        private const string MessageThing = "message";
        
        // Messages
        private const string ParnterIsNeededForSendingMessage = "Partner is needed for the message sending";
        
        // Injects
        private readonly IUtilsService _utilsService;
        private readonly INotificationService _notificationService;
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        private readonly ILoggerService _loggerService;

        /// <summary>
        /// Injector Constructor
        /// </summary>
        /// <param name="utilsService">Utils Service</param>
        /// <param name="notificationService">Notification Service</param>
        /// <param name="context">Database Context</param>
        /// <param name="mapper">Mapper</param>
        /// <param name="loggerService">Logger Service</param>
        public MessageService(IUtilsService utilsService, INotificationService notificationService, DatabaseContext context, IMapper mapper, ILoggerService loggerService)
        {
            _utilsService = utilsService;
            _notificationService = notificationService;
            _context = context;
            _mapper = mapper;
            _loggerService = loggerService;
        }

        /// <summary>
        /// Get current user's messages
        /// </summary>
        /// <param name="friendId">Partner Id</param>
        /// <returns>List of messages</returns>
        public List<MessageDto> GetMessages(int friendId)
        {
            var user = _utilsService.GetCurrentUser();

            var list = _mapper.Map<List<MessageDto>>(user.SentMessages.Union(user.ReceivedMessages).OrderBy(x => x.Date).ToList()).Select(x => { x.IsMine = x.Sender == user.UserName; return x; }).ToList();

            _loggerService.LogInformation(user, nameof(MessageService), GetMessagesAction, list.Select(x => x.Id).ToList());

            return list;
        }

        /// <summary>
        /// Send message to partner
        /// </summary>
        /// <param name="model">Model of message sending</param>
        public void SendMessage(MessageModel model)
        {
            var user = _utilsService.GetCurrentUser();
            var partner = _context.AppUsers.Find(model.PartnerId);

            if (partner == null)
            {
                throw _loggerService.LogInvalidThings(user, nameof(MessageService), MessageThing, ParnterIsNeededForSendingMessage);
            }

            var message = new Message();

            message.SenderId = user.Id;
            message.ReceiverId = model.PartnerId;
            message.Text = model.Message;

            _context.Messages.Add(message);
            _context.SaveChanges();

            _loggerService.LogInformation(user, nameof(MessageService), SendMessageAction, $"{user.Id}-{partner.Id}");
            _notificationService.AddSystemNotificationByType(SystemNotificationType.MessageArrived, partner);
        }
    }
}
