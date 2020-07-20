using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ManagerAPI.Shared.Models
{
    public class FriendRequestFilterModel
    {
        public bool? Type { get; set; }

        [Required]
        public bool DoFiltering { get; set; }
    }
}
