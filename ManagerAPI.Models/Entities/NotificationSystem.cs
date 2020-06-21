using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Models.Entities
{
    public class NotificationSystem
    {
        [Required]
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string ShortName { get; set; }

        public virtual ICollection<NotificationType> Types { get; set; }
    }
}