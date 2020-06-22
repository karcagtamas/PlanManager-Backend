namespace EventManager.Client.Models.Friends
{
    public class FriendRequestResponseModel
    {
        [Required]
        public int RequestId { get; set; }

        [Required]
        public bool Response { get; set; }
    }
}