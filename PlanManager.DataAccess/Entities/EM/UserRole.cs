using System;
using System.ComponentModel.DataAnnotations;

namespace PlanManager.DataAccess.Entities.EM
{
    public class UserRole
    {
        [Required]
        public string UserId { get; set; }
        
        [Required]
        public string RoleId { get; set; }

        [Required]
        public DateTime OwnershipDate { get; set; }

        public virtual User User { get; set; }

        public virtual EventRole Role { get; set; }
    }
}