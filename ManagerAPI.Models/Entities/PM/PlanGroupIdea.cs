using System;
using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Domain.Entities.PM
{
    public class PlanGroupIdea
    {
        [Required]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string Content { get; set; }
        
        [Required]
        public string CreatorId { get; set; }
        
        [Required]
        public DateTime Creation { get; set; }
        
        [Required]
        public int GroupId { get; set; }

        public virtual User Creator { get; set; }

        public virtual PlanGroup Group { get; set; }
    }
}