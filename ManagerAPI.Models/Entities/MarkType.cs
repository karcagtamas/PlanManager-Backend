using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Models.Entities
{
    public class MarkType
    {
        [Required]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(120)]
        public string Title { get; set; }

        public virtual ICollection<PlanGroupPlan> MarkedPlans { get; set; }
    }
}