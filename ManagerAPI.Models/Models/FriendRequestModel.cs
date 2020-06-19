using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ManagerAPI.Models.Models
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
