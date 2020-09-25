﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ManagerAPI.DataAccess;
using ManagerAPI.Domain.Entities;
using ManagerAPI.Domain.Enums;
using ManagerAPI.Services.Services.Interfaces;
using ManagerAPI.Shared.DTOs;
using ManagerAPI.Shared.Models;
using Microsoft.AspNetCore.Identity;

namespace ManagerAPI.Services.Services
{
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
        private const string FriendIdThing = "friend id";

        // Messages
        private const string YouHasOpenFriendRequestWithThisUserMessage = "You has open friend request with this user";
        private const string ThisNameIsYoursMessage = "This name is yours";
        private const string ThisUserDoesNotExistMessage = "This user does not exist";
        private const string ThisUserIsYourFriendAlready = "This user is your friend already";
        private const string ThisUserIsNotYourFriendMessage = "This user is not your friend";
        private const string RequestDoesNotExistMessage = "Request does not exist";
        private const string FriendDoesNotExistMessage = "Friend does not exist";

        // Injects
        private readonly IUtilsService _utilsService;
        private readonly IMapper _mapper;
        private readonly DatabaseContext _context;
        private readonly INotificationService _notificationService;
        private readonly ILoggerService _loggerService;
        private readonly UserManager<User> _userManager;

        /// <summary>
        /// Injector Constructor
        /// </summary>
        /// <param name="utilsService">Utils Service</param>
        /// <param name="mapper">Mapper</param>
        /// <param name="context">Database Context</param>
        /// <param name="notificationService">Notification Service</param>
        /// <param name="loggerService">Logger Service</param>
        /// <param name="userManager">User manager</param>
        public FriendService(IUtilsService utilsService, IMapper mapper, DatabaseContext context,
            INotificationService notificationService, ILoggerService loggerService, UserManager<User> userManager)
        {
            _utilsService = utilsService;
            _mapper = mapper;
            _context = context;
            _notificationService = notificationService;
            _loggerService = loggerService;
            _userManager = userManager;
        }

        /// <summary>
        /// Get current user's got friend requests
        /// </summary>
        /// <returns>List of friend requests</returns>
        public List<FriendRequestListDto> GetMyFriendRequests()
        {
            var user = _utilsService.GetCurrentUser();

            var list = _mapper.Map<List<FriendRequestListDto>>(user.ReceivedFriendRequest.Where(x => x.Response == null)
                .OrderByDescending(x => x.SentDate).ToList());

            _loggerService.LogInformation(user, nameof(FriendService), GetFriendRequestAction,
                list.Select(x => x.Id).ToList());

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

            _loggerService.LogInformation(user, nameof(FriendService), GetFriendsAction,
                list.Select(x => x.FriendId).ToList());

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

            var friends = _context.Friends.Where(x =>
                    x.User.Id == user.Id && x.Friend.Id == friendId || x.User.Id == friendId && x.Friend.Id == user.Id)
                .ToList();

            if (friends.Count == 0)
            {
                throw _loggerService.LogInvalidThings(user, nameof(FriendService), FriendThing,
                    ThisUserIsNotYourFriendMessage);
            }

            _context.Friends.RemoveRange(friends);
            _context.SaveChanges();

            _loggerService.LogInformation(user, nameof(FriendService), RemoveFriendAction, friendId);

            _notificationService.AddSystemNotificationByType(SystemNotificationType.FriendRemoved, user,
                friend.UserName);
            _notificationService.AddSystemNotificationByType(SystemNotificationType.FriendRemoved, friend,
                user.UserName);
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
                throw _loggerService.LogInvalidThings(user, nameof(FriendService), UserNameThing,
                    ThisNameIsYoursMessage);
            }

            var destination = _context.AppUsers.FirstOrDefault(x => x.UserName == model.DestinationUserName);

            if (destination == null)
            {
                throw _loggerService.LogInvalidThings(user, nameof(FriendService), UserNameThing,
                    ThisUserDoesNotExistMessage);
            }

            if (HasFriendAlready(user, destination.Id))
            {
                throw _loggerService.LogInvalidThings(user, nameof(FriendService), UserNameThing,
                    ThisUserIsYourFriendAlready);
            }

            if (HasOpenFriendRequestAlready(user, destination.Id))
            {
                throw _loggerService.LogInvalidThings(user, nameof(FriendService), UserNameThing,
                    YouHasOpenFriendRequestWithThisUserMessage);
            }

            var request = new FriendRequest
            {
                Message = model.Message, SenderId = user.Id, DestinationId = destination.Id
            };


            _context.FriendRequests.Add(request);
            _context.SaveChanges();

            _loggerService.LogInformation(user, nameof(FriendService), SendFriendRequestAction, request.Id);

            _notificationService.AddSystemNotificationByType(SystemNotificationType.FriendRequestSent, user,
                destination.UserName);
            _notificationService.AddSystemNotificationByType(SystemNotificationType.FriendRequestReceived, destination,
                user.UserName);
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
                throw _loggerService.LogInvalidThings(user, nameof(FriendService), RequestIdThing,
                    RequestDoesNotExistMessage);
            }

            request.Response = model.Response;
            request.ResponseDate = DateTime.Now;
            _context.FriendRequests.Update(request);
            _context.SaveChanges();
            _loggerService.LogInformation(user, nameof(FriendService), SendFriendRequestResponseAction, request.Id);
            _notificationService.AddSystemNotificationByType(
                model.Response
                    ? SystemNotificationType.FriendRequestAccepted
                    : SystemNotificationType.FriendRequestDeclined, request.Sender, user.UserName);

            if (model.Response)
            {
                var friend1 = new Friends {RequestId = model.RequestId, UserId = user.Id, FriendId = request.Sender.Id};
                _context.Friends.Add(friend1);

                var friend2 = new Friends {RequestId = model.RequestId, UserId = request.Sender.Id, FriendId = user.Id};
                _context.Friends.Add(friend2);

                _context.SaveChanges();

                _notificationService.AddSystemNotificationByType(SystemNotificationType.YouHasANewFriend, user,
                    request.Sender.UserName);
                _notificationService.AddSystemNotificationByType(SystemNotificationType.YouHasANewFriend,
                    request.Sender, user.UserName);
            }
        }

        /// <summary>
        /// Check the user already has a friend with the given Id
        /// </summary>
        /// <param name="user">User</param>
        /// <param name="friendId">Friend's Id</param>
        /// <returns>User has this friend or not</returns>
        private bool HasFriendAlready(User user, string friendId)
        {
            return user.FriendListLeft.Any(x => x.FriendId == friendId);
        }

        /// <summary>
        /// Check the user already has opened friend request
        /// </summary>
        /// <param name="user">User</param>
        /// <param name="friendId">Friend's Id</param>
        /// <returns>User has friend request or not</returns>
        private bool HasOpenFriendRequestAlready(User user, string friendId)
        {
            return user.SentFriendRequest
                       .FirstOrDefault(x => x.DestinationId == friendId && x.Response == null) != null ||
                   user.ReceivedFriendRequest
                       .FirstOrDefault(x => x.SenderId == friendId && x.Response == null) != null;
        }

        /// <summary>
        /// Get friend data
        /// </summary>
        /// <param name="friendId">Friend's Id</param>
        /// <returns>Friend data</returns>
        public async Task<FriendDataDto> GetFriendData(string friendId)
        {
            var user = _utilsService.GetCurrentUser();

            var friend = await _context.AppUsers.FindAsync(friendId);

            if (friend == null)
            {
                throw this._loggerService.LogInvalidThings(user, nameof(FriendService), FriendIdThing,
                    FriendDoesNotExistMessage);
            }

            var friendDto = _mapper.Map<FriendDataDto>(friend);
            var list = (await _userManager.GetRolesAsync(user)).ToList();
            friendDto.Roles = _context.AppRoles.OrderByDescending(x => x.AccessLevel).Where(x => list.Contains(x.Name))
                .Select(x => x.Name).ToList();
            return friendDto;
        }
    }
}