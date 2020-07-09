using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ManagerAPI.DataAccess;
using ManagerAPI.Models.DTOs;
using ManagerAPI.Models.Entities;
using ManagerAPI.Models.Enums;
using ManagerAPI.Models.Models;
using ManagerAPI.Services.Services.Interfaces;

namespace ManagerAPI.Services.Services {
    /// <summary>
    /// Friend Service
    /// </summary>
    public class FriendService : IFriendService 
    {
        // Actions
        private const string SendFriendRequestResponseAction = "send friend request response";
        private const string RemoveFriendAction = "remove friend";
        private const string SendFriendRequestAction = "send friend request";
        private const string GetFriendsAction = "get friends";
        private const string GetFriendRequestAction = "get friend requests";

        // Things
        private const string RequestIdThing = "request id";
        private const string UserNameThing = "username";
        private const string FriendThing = "friend";

        // Messages
        private const string YouHasOpenFriendRequestWithThisUserMessage = "You has open friend request with this user";
        private const string ThisNameIsYoursMesssage = "This name is yours";
        private const string ThisUserDoesNotExistMessage = "This user does not exist";
        private const string ThisUserIsYourFriendAlready = "This user is your friend already";
        private const string ThisUserIsNotYourFriendMessage = "This user is not your friend";
        private const string RequestDoesNotExistMessage = "Request does not exist";

        // Injects
        private readonly IUtilsService _utilsService;
        private readonly IMapper _mapper;
        private readonly DatabaseContext _context;
        private readonly INotificationService _notificationService;
        private readonly ILoggerService _loggerService;

        /// <summary>
        /// Injector Constructor
        /// </summary>
        /// <param name="utilsService">Utils Service</param>
        /// <param name="mapper">Mapper</param>
        /// <param name="context">Database Context</param>
        /// <param name="notificationService">Notification Service</param>
        public FriendService (IUtilsService utilsService, IMapper mapper, DatabaseContext context, INotificationService notificationService, ILoggerService loggerService) {
            _utilsService = utilsService;
            _mapper = mapper;
            _context = context;
            _notificationService = notificationService;
            _loggerService = loggerService;
        }

        /// <summary>
        /// Get current user's got friend requests
        /// </summary>
        /// <param name="type">Undecided / accepted / declined</param>
        /// <returns>List of friend requests</returns>
        public List<FriendRequestListDto> GetMyFriendRequests () {
            var user = _utilsService.GetCurrentUser ();

            var list = _mapper.Map<List<FriendRequestListDto>> (user.ReceivedFriendRequest.Where (x => x.Response == null).OrderByDescending (x => x.SentDate).ToList ());

            _loggerService.LogInformation (user, nameof(FriendService), GetFriendRequestAction, 0);

            return list;
        }

        /// <summary>
        /// Get current user's friends
        /// Add notification rows
        /// </summary>
        /// <returns>List of friends</returns>
        public List<FriendListDto> GetMyFriends () {
            var user = _utilsService.GetCurrentUser ();

            var list = _mapper.Map<List<FriendListDto>> (user.FriendListLeft.OrderBy (x => x.Friend.UserName).ToList ());

            _loggerService.LogInformation (user, nameof(FriendService), GetFriendsAction, 0);

            return list;
        }

        /// <summary>
        /// Remove current user's friend by Id
        /// Add notification rows
        /// </summary>
        /// <param name="friendId">Friend Id</param>
        public void RemoveFriend (string friendId) {
            var user = _utilsService.GetCurrentUser ();
            var friend = _context.AppUsers.Find (friendId);

            var friends = _context.Friends.Where (x => x.User.Id == user.Id && x.Friend.Id == friendId || x.User.Id == friendId && x.Friend.Id == user.Id).ToList ();

            if (friends.Count == 0) {
                throw _loggerService.LogInvalidThings(user, nameof(FriendService), FriendThing, ThisUserIsNotYourFriendMessage);
            }

            _context.Friends.RemoveRange (friends);
            _context.SaveChanges ();

            _loggerService.LogInformation (user, nameof(FriendService), RemoveFriendAction, friendId);

            _notificationService.AddSystemNotificationByType (SystemNotificationType.FriendRemoved, user);
            _notificationService.AddSystemNotificationByType (SystemNotificationType.FriendRemoved, friend);
        }

        /// <summary>
        /// Send friend request
        /// Add notification rows
        /// </summary>
        /// <param name="model">Model of the request</param>
        public void SendFriendRequest (FriendRequestModel model) {
            var user = _utilsService.GetCurrentUser ();

            if (user.UserName == model.DestinationUserName) {
                throw _loggerService.LogInvalidThings(user, nameof(FriendService), UserNameThing, ThisNameIsYoursMesssage);
            }

            var destination = _context.AppUsers.Where (x => x.UserName == model.DestinationUserName).FirstOrDefault ();

            if (destination == null) {
                throw _loggerService.LogInvalidThings(user, nameof(FriendService), UserNameThing, ThisUserDoesNotExistMessage);
            }

            if (HasFriendAlready (user, destination.Id)) {
                throw _loggerService.LogInvalidThings(user, nameof(FriendService), UserNameThing, ThisUserIsYourFriendAlready);
            }

            if (HasOpenFriendRequestAlready (user, destination.Id)) {
                throw _loggerService.LogInvalidThings(user, nameof(FriendService), UserNameThing, YouHasOpenFriendRequestWithThisUserMessage);
            }

            var request = new FriendRequest ();

            request.Message = model.Message;
            request.SenderId = user.Id;
            request.DestinationId = destination.Id;

            _context.FriendRequests.Add (request);
            _context.SaveChanges ();

            _loggerService.LogInformation (user, nameof(FriendService), SendFriendRequestAction, request.Id);

            _notificationService.AddSystemNotificationByType (SystemNotificationType.FriendRequestSent, user);
            _notificationService.AddSystemNotificationByType (SystemNotificationType.FriendRequestReceived, destination);
        }

        /// <summary>
        /// Send friend request response
        /// Add notification rows
        /// </summary>
        /// <param name="model">Model of response</param>
        public void SendFriendRequestResponse (FriendRequestResponseModel model) {
            var user = _utilsService.GetCurrentUser ();

            var request = _context.FriendRequests.Find (model.RequestId);

            if (request == null) {
                throw _loggerService.LogInvalidThings(user, nameof(FriendService), RequestIdThing, RequestDoesNotExistMessage);
            }

            request.Response = model.Response;
            request.ResponseDate = DateTime.Now;
            _context.FriendRequests.Update (request);
            _context.SaveChanges ();
            _loggerService.LogInformation (user, nameof(FriendService), SendFriendRequestResponseAction, request.Id);
            _notificationService.AddSystemNotificationByType (model.Response ? SystemNotificationType.FriendRequestAccepted : SystemNotificationType.FriendRequestDeclined, request.Sender);

            if (model.Response) {
                var friend1 = new Friends ();
                friend1.RequestId = model.RequestId;
                friend1.UserId = user.Id;
                friend1.FriendId = request.Sender.Id;
                _context.Friends.Add (friend1);

                var friend2 = new Friends ();
                friend2.RequestId = model.RequestId;
                friend2.UserId = request.Sender.Id;
                friend2.FriendId = user.Id;
                _context.Friends.Add (friend2);

                _context.SaveChanges ();

                _notificationService.AddSystemNotificationByType (SystemNotificationType.YouHasANewFriend, user);
                _notificationService.AddSystemNotificationByType (SystemNotificationType.YouHasANewFriend, request.Sender);
            }
        }

        /// <summary>
        /// Check the user already has a friend with the given Id
        /// </summary>
        /// <param name="user">User</param>
        /// <param name="friendId">Friend's Id</param>
        /// <returns>User has this friend or not</returns>
        public bool HasFriendAlready (User user, string friendId) {
            return user.FriendListLeft.Where (x => x.FriendId == friendId).Count () > 0;
        }

        /// <summary>
        /// Check the user already has opened friend request
        /// </summary>
        /// <param name="user">User</param>
        /// <param name="friendId">Friend's Id</param>
        /// <returns>User has friend request or not</returns>
        public bool HasOpenFriendRequestAlready (User user, string friendId) {
            return user.SentFriendRequest.Where (x => x.DestinationId == friendId && x.Response == null).FirstOrDefault () != null ||
                user.ReceivedFriendRequest.Where (x => x.SenderId == friendId && x.Response == null).FirstOrDefault () != null;
        }
    }
}