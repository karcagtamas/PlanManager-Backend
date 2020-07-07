using System;
using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Models.Entities.EM
{
    public class UserEventRole
    {
        [Required]
        public string UserId { get; set; }
        
        [Required]
        public int RoleId { get; set; }

        [Required]
        public DateTime OwnershipDate { get; set; }
        
        [Required] 
        public string AddedById { get; set; }

        public virtual User User { get; set; }

        public virtual EventRole Role { get; set; }

        public virtual User AddedBy { get; set; }
    }
}