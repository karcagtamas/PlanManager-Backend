using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManagerAPI.Domain.Entities.EM
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