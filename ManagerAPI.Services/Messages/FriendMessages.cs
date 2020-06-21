using System;
using System.Collections.Generic;
using System.Text;

namespace ManagerAPI.Services.Messages
{
    public class FriendMessages
    {
        // Invalid
        public static string InvalidUserName = "Invalid UserName";
        public static string InvalidRequestId = "Invalid Request Id";
        public static string AlreadyHasFriend = "This User Already Is Your Friend";
        public static string AlreadyHasOpenFriendRequest = "You Already Have Opened Friend Request With This User";
        public static string ThisUserNameIsYours = "This UserName Is Yours";
        public static string ThisFriendIsNotExist = "This Friend Is Not Exist";

        // Actions
        public static string MyFriendRequestsGet = "my friend requests got";
        public static string MyFriendsGet = "my friends got";
        public static string FriendRemove = "friend removed";
        public static string FriendRequestSend = "friend request sent";
        public static string FriendRequestResponseSend = "friend request response sent";
    }
}
