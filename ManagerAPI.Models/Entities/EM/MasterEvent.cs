using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Domain.Entities.EM
{
    public class MasterEvent
    {
        [Required]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(256)]
        public string Title { get; set; }
        
        public string Description { get; set; }

        [Required]
        public bool IsLocked { get; set; }
        
        [Required]
        public bool IsDisabled { get; set; }
        
        [Required]
        public bool IsPublic { get; set; }
        
        [Required]
        public string CreatorId { get; set; }
        
        [Required]
        public DateTime CreationDate { get; set; }
        
        [Required] 
        public string LastUpdaterId { get; set; }
        
        [Required] 
        public DateTime LastUpdate { get; set; }
        
        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public virtual User Creator { get; set; }

        public virtual User LastUpdater { get; set; }

        public virtual DSportEvent SportEvent { get; set; }
        
        public virtual DGtEvent GtEvent { get; set; }

        public virtual ICollection<UserEvent> Users { get; set; }

        public virtual ICollection<EventRole> Roles { get; set; }

        public virtual ICollection<EventAction> Actions { get; set; }
    }
}