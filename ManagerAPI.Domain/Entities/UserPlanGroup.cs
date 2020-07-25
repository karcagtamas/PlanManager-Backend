using System;
using System.ComponentModel.DataAnnotations;
using ManagerAPI.Domain.Entities.PM;

namespace ManagerAPI.Domain.Entities
{
    public class UserPlanGroup
    {
        [Required]
        public string UserId { get; set; }
        
        [Required]
        public int GroupId { get; set; }
        
        [Required]
        public int RoleId { get; set; }
        
        [Required]
        public DateTime Connection { get; set; }
        
        [Required]
        public string AddedById { get; set; }

        public virtual User User { get; set; }

        public virtual PlanGroup Group { get; set; }

        public virtual GroupRole Role { get; set; }

        public virtual User AddedBy { get; set; }
    }
}