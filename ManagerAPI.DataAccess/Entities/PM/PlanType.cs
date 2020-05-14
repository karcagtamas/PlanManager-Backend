using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.DataAccess.Entities.PM
{
    public class PlanType
    {
        [Required]
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }

        public virtual ICollection<Plan> Plans { get; set; }

        public virtual ICollection<PlanGroupPlan> GroupPlans { get; set; }
    }
}