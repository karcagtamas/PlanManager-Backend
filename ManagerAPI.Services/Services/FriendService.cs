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

namespace ManagerAPI.Services.Services
{
    /// <summary>
    /// Friend Service
    /// </summary>
    public class FriendService : IFriendService
    {
        private IUtilsService _utilsService { get; }
        private IMapper _mapper { get; }
        private DatabaseContext _context { get; }
        private INotificationService _notificationService { get; }

        /// <summary>
        /// Injector Constructor
        /// </summary>
        /// <param name="utilsService">Utils Service</param>
        /// <param name="mapper">Mapper</param>
        /// <param name="context">Database Context</param>
        /// <param name="notificationService">Notification Service</param>
        public FriendService(IUtilsService utilsService, IMapper mapper, DatabaseContext context, INotificationService notificationService)
        {
            _utilsService = utilsService;
            _mapper = mapper;
            _context = context;
            _notificationService = notificationService;
        }

        /// <summary>
        /// Get current user's got friend requests
        /// </summary>
        /// <param name="type">Undecided / accepted / declined</param>
        /// <returns>List of friend requests</returns>
        public List<FriendRequestListDto> GetMyFriendRequests(FriendRequestFilterModel model)
        {
            var user = _utilsService.GetCurrentUser();

            var list = _mapper.Map<List<FriendRequestListDto>>(user.ReceivedFriendRequest.Where(x => x.Response == model.Type).OrderByDescending(x => x.SentDate).ToList());

            _utilsService.LogInformation(FriendMessages.MyFriendRequestsGet, user);

            return list;
        }

        /// <summary>
        /// Get current user's friends
        /// Add notification rows
        /// </summary>
        /// <returns>List of friends</returns>
        public List<FriendListDto> GetMyFriends()
        {
            var user = _utilsService.GetCurrentUser();

            var list = _mapper.Map<List<FriendListDto>>(user.FriendListLeft.OrderBy(x => x.Friend.UserName).ToList());

            _utilsService.LogInformation(FriendMessages.MyFriendsGet, user);

            return list;
        }

        /// <summary>
        /// Remove current user's friend by Id
        /// Add notification rows
        /// </summary>
        /// <param name="friendId">Friend Id</param>
        public void RemoveFriend(string friendId)
        {
            var user = _utilsService.GetCurrentUser();
            var friend = _context.AppUsers.Find(friendId);

            var friends = _context.Friends.Where(x => x.User.Id == user.Id && x.Friend.Id == friendId || x.User.Id == friendId && x.Friend.Id == user.Id).ToList();

            if (friends.Count == 0)
            {
                throw new Exception(FriendMessages.ThisFriendIsNotExist);
            }

            _context.Friends.RemoveRange(friends);
            _context.SaveChanges();

            _utilsService.LogInformation(FriendMessages.FriendRemove, user);

            _notificationService.AddSystemNotificationByType(SystemNotificationType.FriendRemoved, user);
            _notificationService.AddSystemNotificationByType(SystemNotificationType.FriendRemoved, friend);
        }

        /// <summary>
        /// Send friend request
        /// Add notification rows
        /// </summary>
        /// <param name="model">Model of the request</param>
        public void SendFriendRequest(FriendRequestModel model)
        {
            var user = _utilsService.GetCurrentUser();

            if (user.UserName == model.DestinationUserName)
            {
                throw new Exception(FriendMessages.ThisUserNameIsYours);
            }

            var destination = _context.AppUsers.Where(x => x.UserName == model.DestinationUserName).FirstOrDefault();

            if (destination == null)
            {
                throw new Exception(FriendMessages.InvalidUserName);
            }

            if (HasFriendAlready(user, destination.Id))
            {
                throw new Exception(FriendMessages.AlreadyHasFriend);
            }

            if (HasOpenFriendRequestAlready(user, destination.Id))
            {
                throw new Exception(FriendMessages.AlreadyHasOpenFriendRequest);
            }

            var request = new FriendRequest();

            request.Message = model.Message;
            request.SenderId = user.Id;
            request.DestinationId = destination.Id;

            _context.FriendRequests.Add(request);
            _context.SaveChanges();

            _utilsService.LogInformation(FriendMessages.FriendRequestSend, user);

            _notificationService.AddSystemNotificationByType(SystemNotificationType.FriendRequestSent, user);
            _notificationService.AddSystemNotificationByType(SystemNotificationType.FriendRequestReceived, destination);
        }

        /// <summary>
        /// Send friend request response
        /// Add notification rows
        /// </summary>
        /// <param name="model">Model of response</param>
        public void SendFriendRequestResponse(FriendRequestResponseModel model)
        {
            var user = _utilsService.GetCurrentUser();

            var request = _context.FriendRequests.Find(model.RequestId);

            if (request == null)
            {
                throw new Exception(FriendMessages.InvalidRequestId);
            }

            request.Response = model.Response;
            request.ResponseDate = DateTime.Now;
            _context.FriendRequests.Update(request);
            _context.SaveChanges();
            _utilsService.LogInformation(FriendMessages.FriendRequestResponseSend, user);
            _notificationService.AddSystemNotificationByType(model.Response ? SystemNotificationType.FriendRequestAccepted : SystemNotificationType.FriendRequestDeclined, request.Sender);

            if (model.Response)
            {
                var friend1 = new Friends();
                friend1.RequestId = model.RequestId;
                friend1.UserId = user.Id;
                friend1.FriendId = request.Sender.Id;
                _context.Friends.Add(friend1);

                var friend2 = new Friends();
                friend2.RequestId = model.RequestId;
                friend2.UserId = request.Sender.Id;
                friend2.FriendId = user.Id;
                _context.Friends.Add(friend2);

                _context.SaveChanges();

                _notificationService.AddSystemNotificationByType(SystemNotificationType.YouHasANewFriend, user);
                _notificationService.AddSystemNotificationByType(SystemNotificationType.YouHasANewFriend, request.Sender);
            }
        }

        /// <summary>
        /// Check the user already has a friend with the given Id
        /// </summary>
        /// <param name="user">User</param>
        /// <param name="friendId">Friend's Id</param>
        /// <returns>User has this friend or not</returns>
        public bool HasFriendAlready(User user, string friendId)
        {
            return user.FriendListLeft.Where(x => x.FriendId == friendId).Count() > 0;
        }

        /// <summary>
        /// Check the user already has opened friend request
        /// </summary>
        /// <param name="user">User</param>
        /// <param name="friendId">Friend's Id</param>
        /// <returns>User has friend request or not</returns>
        public bool HasOpenFriendRequestAlready(User user, string friendId)
        {
            return user.SentFriendRequest.Where(x => x.DestinationId == friendId && x.Response != null).FirstOrDefault() != null 
                || user.ReceivedFriendRequest.Where(x => x.SenderId == friendId && x.Response != null).FirstOrDefault() != null;
        }
    }
}
