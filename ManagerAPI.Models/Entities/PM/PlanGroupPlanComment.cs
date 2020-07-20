using System;
using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Domain.Entities.PM
{
    public class PlanGroupPlanComment
    {
        [Required]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(256)]
        public string Comment { get; set; }
        
        [Required]
        public string SenderId { get; set; }
        
        [Required]
        public DateTime Creation { get; set; }
        
        [Required]
        public int PlanId { get; set; }

        public virtual User Sender { get; set; }

        public virtual PlanGroupPlan Plan { get; set; }
    }
}