using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace PlanManager.DataAccess.Entities {
    public class User : IdentityUser {
        [MaxLength (100)]
        public string FullName { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        [Column (TypeName = "datetime2")]
        public DateTime RegistrationDate { get; set; }

        [Required]
        [Column (TypeName = "datetime2")]
        public DateTime LastLogin { get; set; }
        
        [EmailAddress] 
        public string SecondaryEmail { get; set; }

        public virtual ICollection<Plan> Plans { get; set; }

        public virtual ICollection<PlanGroup> CreatedPlanGroups { get; set; }

        public virtual ICollection<PlanGroup> LastUpdatedPlanGroups { get; set; }

        public virtual ICollection<PlanGroupIdea> CreatedPlanGroupIdeas { get; set; }

        public virtual ICollection<PlanGroupChatMessage> SentPlanGroupChatMessages { get; set; }

        public virtual ICollection<PlanGroupPlan> MarkedOnGroupPlans { get; set; }

        public virtual ICollection<PlanGroupPlan> CreatedPlanGroupPlans { get; set; }
        public virtual ICollection<PlanGroupPlan> LastUpdatedPlanGroupPlans { get; set; }

        public virtual ICollection<PlanGroupPlanComment> CreatedPlanGroupPlanComment { get; set; }

        public virtual ICollection<UserPlanGroup> Groups { get; set; }

        public virtual ICollection<UserPlanGroup> AddedUsersToGroups { get; set; }
    }
}