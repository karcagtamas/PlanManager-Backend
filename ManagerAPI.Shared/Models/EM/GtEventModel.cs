using ManagerAPI.Shared.DTOs.EM;
using System.ComponentModel.DataAnnotations;

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
            Id = gtEvent.Id;
            TShirtColor = gtEvent.TShirtColor;
            Greeny = gtEvent.Greeny;
            GreenyCost = gtEvent.GreenyCost;
        }
    }
}