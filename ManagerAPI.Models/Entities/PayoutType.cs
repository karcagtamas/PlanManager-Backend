using System.ComponentModel.DataAnnotations;

namespace EventManager.Models.Entities
{
    public class PayoutType
    {
        [Required]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(120)]
        public string Title { get; set; }
        
        [Required]
        public int Direction { get; set; }
    }
}