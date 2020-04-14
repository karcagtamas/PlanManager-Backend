using System.ComponentModel.DataAnnotations;

namespace PlanManager.DataAccess.Entities.EM
{
    public class DGtEvent
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int EventId { get; set; }

        [Required]
        public string TShirtColor { get; set; }

        public int? Greeny { get; set; }

        public decimal? GreenyCost { get; set; }

        public virtual MasterEvent Event { get; set; }
    }
}