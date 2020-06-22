namespace EventManager.Client.Models.Friends
{
    public class FriendRequestModel
    {
        [Required]
        public string DestinationUserName { get; set; }

        [Required]
        [MaxLength(120)]
        public string Message { get; set; }
    }
}