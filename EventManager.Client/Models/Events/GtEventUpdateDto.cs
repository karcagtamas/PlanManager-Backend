using System.ComponentModel.DataAnnotations;

namespace EventManager.Client.Models.Events
{
    public class GtEventUpdateDto
    {
        [Required]
        public int Id { get; set; }
        
        public string TShirtColor { get; set; }

        public int? Greeny { get; set; }
        
        public decimal? GreenyCost { get; set; }
        
        public GtEventUpdateDto() {}

        public GtEventUpdateDto(GtEventDto gtEvent)
        {
            this.Id = gtEvent.Id;
            this.TShirtColor = gtEvent.TShirtColor;
            this.Greeny = gtEvent.Greeny;
            this.GreenyCost = gtEvent.GreenyCost;
        }
    }
}