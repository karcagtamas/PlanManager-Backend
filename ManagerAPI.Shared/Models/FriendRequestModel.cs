using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ManagerAPI.Shared.Models {
    public class FriendRequestModel {
        [Required(ErrorMessage = "User name is required")]
        public string DestinationUserName { get; set; }

        [Required(ErrorMessage = "Message is required")]
        [MaxLength(120, ErrorMessage = "Max length is 120")]
        public string Message { get; set; }
    }
}