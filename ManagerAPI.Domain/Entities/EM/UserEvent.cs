using System;
using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Domain.Entities.EM
{
    public class UserEvent
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public int EventId { get; set; }
        
        [Required] 
        public DateTime ConnectionDate { get; set; }

        [Required]
        public string AddedById { get; set; }

        public virtual User User { get; set; }
        
        public virtual MasterEvent Event { get; set; }
        
        public virtual User AddedBy { get; set; }
    }
}