using System;
using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Models.Entities.EM
{
    public class Payout
    {
        [Required]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(256)]
        public string Title { get; set; }
        
        [Required]
        public int EventId { get; set; }
        
        [Required]
        public int TypeId { get; set; }
        
        [Required]
        public decimal Cost { get; set; }
        
        [Required]
        [MaxLength(120)]
        public string From { get; set; }
        
        [Required]
        [MaxLength(120)]
        public string To { get; set; }
        
        [Required]
        public string CreatorId { get; set; }
        
        [Required]
        public DateTime CreationDate { get; set; }
        
        [Required]
        public string LastUpdaterId { get; set; }
        
        [Required]
        public DateTime LastUpdate { get; set; }

        public virtual PayoutType Type { get; set; }

        public virtual MasterEvent Event { get; set; }

        public virtual User Creator { get; set; }
        
        public virtual User LastUpdater { get; set; }
    }
}