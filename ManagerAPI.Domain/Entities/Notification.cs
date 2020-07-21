using System;
using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Domain.Entities
{
    public class Notification
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(256)]
        public string Content { get; set; }

        [Required]
        public DateTime SentDate { get; set; }
        
        [Required] 
        public string OwnerId { get; set; }
        
        [Required] 
        public bool IsRead { get; set; }
        
        [Required]
        public bool Archived { get; set; }
        
        [Required]
        public int TypeId { get; set; }

        public virtual User Owner { get; set; }
        public virtual NotificationType Type { get; set; }
    }
}