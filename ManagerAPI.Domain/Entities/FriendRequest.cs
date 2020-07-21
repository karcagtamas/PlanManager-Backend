using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ManagerAPI.Domain.Entities
{
    public class FriendRequest
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string SenderId { get; set; }

        [Required]
        public string DestinationId { get; set; }

        [Required]
        public DateTime SentDate { get; set; }

        [Required]
        [MaxLength(120)]
        public string Message { get; set; }

        public bool? Response { get; set; }

        public DateTime? ResponseDate { get; set; }

        public virtual User Sender { get; set; }

        public virtual User Destination { get; set; }

        public virtual ICollection<Friends> FriendCollection { get; set; }
    }
}
