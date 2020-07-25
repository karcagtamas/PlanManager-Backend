using System;
using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Domain.Entities
{
    public class News
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(512)]
        public string Content { get; set; }

        [Required]
        public string CreatorId { get; set; }
        
        [Required]
        public DateTime Creation { get; set; }
        
        [Required]
        public string LastUpdaterId { get; set; }
        
        [Required]
        public DateTime LastUpdate { get; set; }

        public virtual User Creator { get; set; }
        
        public virtual User LastUpdater { get; set; }
    }
}