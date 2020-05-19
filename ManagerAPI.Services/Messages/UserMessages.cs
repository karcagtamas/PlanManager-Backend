namespace ManagerAPI.Services.Messages
{
    public class UserMessages
    {
        // Invalid responses
        public readonly string InvalidUserId = "Invalid user Id.";
        public readonly string InvalidUserUpdate = "Invalid user update data.";
        public readonly string InvalidImage = "Invalid image update data.";
        
        // Actions
        public readonly string UserUpdate = "user update";
        public readonly string UserGet = "user get";
        public readonly string GendersGet = "gender get";
        public readonly string ProfileImageUpdate = "profile image update";
    }
}