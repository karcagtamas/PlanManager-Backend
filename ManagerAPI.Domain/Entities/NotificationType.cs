using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Domain.Entities
{
    public class NotificationType
    {
        [Required]
        public int Id { get; set; }
        
        [Required] 
        public string Title { get; set; }
        
        [Required]
        public int ImportanceLevel { get; set; }

        [Required]
        public int SystemId { get; set; }

        public virtual NotificationSystem System { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
    }
}