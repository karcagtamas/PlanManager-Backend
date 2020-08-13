using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Shared.Models
{
    /// <summary>
    /// Friend request response model
    /// </summary>
    public class FriendRequestResponseModel
    {
        [Required] public int RequestId { get; set; }

        [Required] public bool Response { get; set; }
    }
}