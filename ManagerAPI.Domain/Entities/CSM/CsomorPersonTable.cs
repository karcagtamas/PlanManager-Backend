using System;
using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Domain.Entities.CSM
{
    public class CsomorPersonTable
    {
        [Required]
        public int Id { get; set; }
        
        [Required]
        public DateTime Date { get; set; }
        
        [Required]
        public string PersonId { get; set; }
        
        [Required]
        public bool IsAvailable { get; set; }
        
        public string WorkId { get; set; }
        
        public virtual CsomorPerson Person { get; set; }
        public virtual CsomorWork Work { get; set; }
    }
}