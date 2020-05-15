using System;
using System.ComponentModel.DataAnnotations;
using EventManager.Models.Entities;

namespace ManagerAPI.Models.Entities
{
    public class EventAction
    {
        [Required]
        public int Id { get; set; }
        
        public DateTime Date { get; set; }
        
        [Required]
        public string Message { get; set; }
        
        [Required]
        public int EventId { get; set; }
        
        [Required]
        public string UserId { get; set; }
        
        public virtual User User { get; set; }
        
        public virtual MasterEvent Event { get; set; }
    }
}