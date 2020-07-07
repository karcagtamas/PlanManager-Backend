using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Models.DTOs.EM
{
    public class GtEventUpdateDto
    {
        [Required]
        public int Id { get; set; }
        
        public string TShirtColor { get; set; }

        public int? Greeny { get; set; }
        
        public decimal? GreenyCost { get; set; }
    }
}