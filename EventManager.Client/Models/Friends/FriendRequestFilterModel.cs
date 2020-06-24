using System.ComponentModel.DataAnnotations;

namespace EventManager.Client.Models.Friends
{
    public class FriendRequestFilterModel
    {
        public bool? Type { get; set; }

        [Required]
        public bool DoFiltering { get; set; }
    }
}