using System;
using System.ComponentModel.DataAnnotations;

namespace PlanManager.DataAccess.Entities
{
    public class PlanGroup
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        
        [Required]
        public string CreatorId { get; set; }
        
        [Required]
        public DateTime Creation { get; set; }
        
        [Required]
        public string LastUpdaterId { get; set; }
        
        [Required]
        public DateTime LastUpdate { get; set; }
        
        [Required]
        public bool IsArchived { get; set; }

        public virtual User Creator { get; set; }
        
        public virtual User LastUpdater { get; set; }
    }
}