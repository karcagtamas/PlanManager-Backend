using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PlanManager.DataAccess.Entities.EM
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