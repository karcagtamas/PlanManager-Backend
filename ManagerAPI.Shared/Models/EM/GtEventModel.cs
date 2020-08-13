using System.ComponentModel.DataAnnotations;
using ManagerAPI.Shared.DTOs.EM;

namespace ManagerAPI.Shared.Models.EM
{
    /// <summary>
    /// Gt event create or update model
    /// </summary>
    public class GtEventModel
    {
        [Required] public int Id { get; set; }
        public string TShirtColor { get; set; }
        public int? Greeny { get; set; }
        public decimal? GreenyCost { get; set; }

        /// <summary>
        /// Empty init
        /// </summary>
        public GtEventModel()
        {
        }

        /// <summary>
        /// Model from event data object
        /// </summary>
        /// <param name="gtEvent">Gt event data object</param>
        public GtEventModel(GtEventDto gtEvent)
        {
            this.Id = gtEvent.Id;
            this.TShirtColor = gtEvent.TShirtColor;
            this.Greeny = gtEvent.Greeny;
            this.GreenyCost = gtEvent.GreenyCost;
        }
    }
}