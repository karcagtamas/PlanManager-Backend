using System;
using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.DataAccess.Entities.PM
{
    public class Plan
    {
        [Required]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(150)]
        public string Title { get; set; }
        
        [MaxLength(512)]
        public string Description { get; set; }
        
        [Required]
        public string OwnerId { get; set; }
        
        [Required]
        public DateTime Creation { get; set; }
        
        [Required]
        public DateTime LastUpdate { get; set; }
        
        [Required]
        public DateTime StartTime { get; set; }
        
        public DateTime? EndTime { get; set; }
        
        public int? PriorityLevel { get; set; }
        
        [Required]
        public bool IsPublic { get; set; }
        
        public int? PlanTypeId { get; set; }

        public virtual User Owner { get; set; }
        public virtual PlanType PlanType { get; set; }
    }
}