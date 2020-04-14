using System;
using System.ComponentModel.DataAnnotations;

namespace PlanManager.DataAccess.Entities
{
    public class Notification
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(256)]
        public string Message { get; set; }

        [Required]
        public DateTime SentDate { get; set; }
        
        [Required] 
        public string OwnerId { get; set; }
        
        [Required] 
        public bool IsRead { get; set; }

        public virtual User Owner { get; set; }
    }
}