namespace ManagerAPI.Services.Messages
{
    public class UserMessages
    {
        // Invalid responses
        public readonly string InvalidUserId = "Invalid user Id.";
        public readonly string InvalidUserUpdate = "Invalid user update data.";
        public readonly string InvalidImage = "Invalid image update data.";
        public readonly string InvalidOldPassword = "Invalid old password.";
        public readonly string OldAndNewPasswordCannotBeSame = "Old and new password cannot be same.";
        public readonly string AlreadyOwnThisUsername = "You already own this username.";
        
        // Actions
        public readonly string UserUpdate = "user update";
        public readonly string UserGet = "user get";
        public readonly string UserShortGet = "user short get";
        public readonly string GendersGet = "gender get";
        public readonly string ProfileImageUpdate = "profile image update";
        public readonly string UsernameUpdate = "username update";
        public readonly string PasswordUpdate = "password update";
        public readonly string DisableStatus = "disable status";
    }
}