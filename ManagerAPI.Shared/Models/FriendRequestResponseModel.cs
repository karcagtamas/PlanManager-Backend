using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Shared.Models
{
    public class FriendRequestResponseModel
    {
        [Required]
        public int RequestId { get; set; }

        [Required]
        public bool Response { get; set; }
    }
}