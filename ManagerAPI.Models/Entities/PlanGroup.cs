#nullable enable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Models.Entities
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
        
        public string? LastUpdaterId { get; set; }
        
        [Required]
        public DateTime LastUpdate { get; set; }
        
        [Required]
        public bool IsArchived { get; set; }

        public virtual User Creator { get; set; }
        
        public virtual User LastUpdater { get; set; }

        public virtual ICollection<PlanGroupIdea> Ideas { get; set; }

        public virtual ICollection<PlanGroupChatMessage> Messages { get; set; }

        public virtual ICollection<PlanGroupPlan> Plans { get; set; }

        public virtual ICollection<UserPlanGroup> Users { get; set; }
    }
}