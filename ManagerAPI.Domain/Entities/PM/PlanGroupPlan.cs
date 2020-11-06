using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Domain.Entities.PM
{
    public class PlanGroupPlan
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

        public string? LastUpdaterId { get; set; }

        public DateTime? LastUpdate { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public int? PriorityLevel { get; set; }

        [Required]
        public bool IsPublic { get; set; }

        public int? PlanTypeId { get; set; }

        public string MarkedUserId { get; set; }

        public int? MarkTypeId { get; set; }

        public int GroupId { get; set; }

        public virtual User Owner { get; set; }
        public virtual User LastUpdater { get; set; }
        public virtual PlanType PlanType { get; set; }
        public virtual User MarkedUser { get; set; }
        public virtual MarkType MarkType { get; set; }
        public virtual PlanGroup Group { get; set; }

        public virtual ICollection<PlanGroupPlanComment> Comments { get; set; }
    }
}