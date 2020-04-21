using System;
using System.ComponentModel.DataAnnotations;

namespace PlanManager.DataAccess.Entities.PM
{
    public class PlanGroupChatMessage
    {
        [Required]
        public int Id { get; set; }
        
        [Required]
        public string Message { get; set; }
        
        [Required]
        public string SenderId { get; set; }
        
        [Required]
        public DateTime Sent { get; set; }

        [Required]
        public int GroupId { get; set; }

        public virtual User Sender { get; set; }

        public virtual PlanGroup Group { get; set; }
    }
}