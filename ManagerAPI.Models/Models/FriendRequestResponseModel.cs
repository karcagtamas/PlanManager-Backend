using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Models.Models
{
    public class FriendRequestResponseModel
    {
        [Required]
        public int RequestId { get; set; }

        [Required]
        public bool Response { get; set; }
    }
}