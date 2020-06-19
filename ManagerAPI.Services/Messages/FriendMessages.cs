using System;
using System.Collections.Generic;
using System.Text;

namespace ManagerAPI.Services.Messages
{
    public class FriendMessages
    {
        // Invalid
        public readonly string InvalidUserName = "Invalid UserName";
        public readonly string InvalidRequestId = "Invalid Request Id";

        // Actions
        public readonly string MyFriendRequestsGet = "my friend requests got";
        public readonly string MyFriendsGet = "my friends got";
        public readonly string FriendRemove = "friend removed";
        public readonly string FriendRequestSend = "friend request sent";
        public readonly string FriendRequestResponseSend = "friend request response sent";
    }
}
