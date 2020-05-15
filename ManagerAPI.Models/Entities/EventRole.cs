using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EventManager.Models.Entities;

namespace ManagerAPI.Models.Entities
{
    public class EventRole
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(120)]
        public string Title { get; set; }

        [Required]
        public int AccessLevel { get; set; }
        
        [Required] 
        public int EventId { get; set; }

        public virtual MasterEvent Event { get; set; }

        public virtual ICollection<UserEventRole> Users { get; set; }
    }
}