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
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ManagerAPI.Services.Services
{
    public class FriendService : IFriendService
    {
        private IUtilsService UtilsService { get; }
        private IMapper Mapper { get; }
        private DatabaseContext Context { get; }
        private NotificationService NotificationService { get; }
        private FriendMessages FriendMessages { get; set; }

        public FriendService(IUtilsService utilsService, IMapper mapper, DatabaseContext context, NotificationService notificationService)
        {
            UtilsService = utilsService;
            Mapper = mapper;
            Context = context;
            NotificationService = notificationService;
        }


        public List<FriendRequestListDto> GetMyFriendRequests(bool? type)
        {
            var user = UtilsService.GetCurrentUser();

            var list = Mapper.Map<List<FriendRequestListDto>>(user.ReceivedFriendRequest.Where(x => x.Response == type).OrderByDescending(x => x.SentDate).ToList());

            UtilsService.LogInformation(FriendMessages.MyFriendRequestsGet, user);

            return list;
        }

        public List<FriendListDto> GetMyFriends()
        {
            var user = UtilsService.GetCurrentUser();

            var list = Mapper.Map<List<FriendListDto>>(user.FriendListLeft.OrderBy(x => x.Friend.UserName).ToList());

            UtilsService.LogInformation(FriendMessages.MyFriendsGet, user);

            return list;
        }

        public void RemoveFriend(string friendId)
        {
            var user = UtilsService.GetCurrentUser();
            var friend = Context.AppUsers.Find(friendId);

            var requests = Context.Friends.Where(x => x.User.Id == user.Id && x.Friend.Id == friendId || x.User.Id == friendId && x.Friend.Id == user.Id).ToList();

            Context.Friends.RemoveRange(requests);
            Context.SaveChanges();

            UtilsService.LogInformation(FriendMessages.FriendRemove, user);

            NotificationService.AddSystemNotificationByType(SystemNotificationType.FriendRemoved, user);
            NotificationService.AddSystemNotificationByType(SystemNotificationType.FriendRemoved, friend);
        }

        public void SendFriendRequest(FriendRequestModel model)
        {
            var user = UtilsService.GetCurrentUser();

            var destination = Context.AppUsers.Where(x => x.UserName == model.DestinationUserName).FirstOrDefault();

            if (destination == null)
            {
                throw new Exception(FriendMessages.InvalidUserName);
            }

            var request = new FriendRequest();

            request.Message = model.Message;
            request.SenderId = user.Id;
            request.DestinationId = destination.Id;

            Context.FriendRequests.Add(request);
            Context.SaveChanges();

            UtilsService.LogInformation(FriendMessages.FriendRequestSend, user);

            NotificationService.AddSystemNotificationByType(SystemNotificationType.FriendRequestSent, user);
            NotificationService.AddSystemNotificationByType(SystemNotificationType.FriendRequestReceived, destination);
        }

        public void SendFriendRequestResponse(FriendRequestResponseModel model)
        {
            var user = UtilsService.GetCurrentUser();

            var request = Context.FriendRequests.Find(model.RequestId);

            if (request == null)
            {
                throw new Exception(FriendMessages.InvalidRequestId);
            }

            request.Response = model.Response;
            request.ResponseDate = DateTime.Now;
            Context.FriendRequests.Update(request);
            Context.SaveChanges();
            UtilsService.LogInformation(FriendMessages.FriendRequestResponseSend, user);
            NotificationService.AddSystemNotificationByType(model.Response ? SystemNotificationType.FriendRequestAccepted : SystemNotificationType.FriendRequestDeclined, request.Sender);

            if (model.Response)
            {
                var friend1 = new Friends();
                friend1.RequestId = model.RequestId;
                friend1.UserId = user.Id;
                friend1.FriendId = request.Sender.Id;
                Context.Friends.Update(friend1);

                var friend2 = new Friends();
                friend2.RequestId = model.RequestId;
                friend2.UserId = request.Sender.Id;
                friend2.FriendId = user.Id;
                Context.Friends.Update(friend2);

                Context.SaveChanges();

                NotificationService.AddSystemNotificationByType(SystemNotificationType.YouHasANewFriend, user);
                NotificationService.AddSystemNotificationByType(SystemNotificationType.YouHasANewFriend, request.Sender);
            }
        }
    }
}
