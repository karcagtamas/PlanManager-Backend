using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ManagerAPI.Models.Entities
{
    public class Friends
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string FriendId { get; set; }

        [Required]
        public DateTime ConnectionDate { get; set; }

        [Required]
        public int RequestId { get; set; }

        public virtual User User { get; set; }

        public virtual User Friend { get; set; }

        public virtual FriendRequest Request { get; set; }
    }
}
