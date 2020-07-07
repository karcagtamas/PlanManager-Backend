using System;
using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Models.DTOs.EM
{
    public class MasterEventUpdateDto
    {
        [Required]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(256)]
        public string Title { get; set; }

        public string Description { get; set; }
        
        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
        
        [Required]
        public bool IsLocked { get; set; }
        
        [Required]
        public bool IsDisabled { get; set; }
        
        [Required]
        public bool IsPublic { get; set; }
    }
}