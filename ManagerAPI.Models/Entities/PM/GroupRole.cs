using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Domain.Entities.PM
{
    public class GroupRole
    {
        [Required]
        public int Id { get; set; }
        
        [Required]
        public string Title { get; set; }
        
        [Required]
        public int AccessLevel { get; set; }

        public virtual ICollection<UserPlanGroup> GroupMembers { get; set; }
    }
}