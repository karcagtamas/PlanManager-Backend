using System;
using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Domain.Entities.CSM
{
    public class UserCsomor
    {
        [Required]
        public string UserId { get; set; }
        
        [Required]
        public int CsomorId { get; set; }
        
        [Required]
        public DateTime SharedOn { get; set; }
        
        [Required]
        public bool HasWriteAccess { get; set; }
        
        public virtual User User { get; set; }
        public virtual Csomor Csomor { get; set; }
    }
}