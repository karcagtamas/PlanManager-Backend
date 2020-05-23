using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EventManager.Models.Entities;

namespace ManagerAPI.Models.Entities
{
    public class DGtEvent
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int EventId { get; set; }
        
        public string TShirtColor { get; set; }

        public int? Greeny { get; set; }

        [Column(TypeName = "decimal(10,4)")]
        public decimal? GreenyCost { get; set; }

        public virtual MasterEvent Event { get; set; }
    }
}