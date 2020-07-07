using System;
using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Models.Entities.EM
{
    public class EventMessage
    {
        [Required]
        public int Id { get; set; }
        
        [Required]
        public string MessageContent { get; set; }
        
        [Required]
        public int EventId { get; set; }

        [Required]
        public string SenderId { get; set; }
        
        [Required]
        public DateTime SentDate { get; set; }

        public virtual MasterEvent Event { get; set; }

        public virtual User Sender { get; set; }
    }
}