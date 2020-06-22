namespace EventManager.Client.Models.Friends
{
    public class FriendListDto
    {
        [Required]
        public string Friend { get; set; }

        [Required]
        public DateTime ConnectionDate { get; set; }
    }
}