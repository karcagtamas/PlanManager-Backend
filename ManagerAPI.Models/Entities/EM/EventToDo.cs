using System;
using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Models.Entities.EM
{
    public class EventToDo
    {
        [Required]
        public int Id { get; set; }
        
        [Required]
        public string Text { get; set; }
        
        [Required]
        public int EventId { get; set; }

        [Required]
        public string CreatorId { get; set; }
        
        [Required]
        public DateTime CreationDate { get; set; }
        
        [Required]
        public string LastUpdaterId { get; set; }
        
        [Required]
        public DateTime LastUpdate { get; set; }
        
        [Required]
        public bool IsSolved { get; set; }

        public virtual MasterEvent Event { get; set; }

        public virtual User Creator { get; set; }
        
        public virtual User LastUpdater { get; set; }
    }
}