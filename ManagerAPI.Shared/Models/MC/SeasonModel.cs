using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Shared.Models.MC
{
    public class SeasonModel
    {
        [Required]
        public int Number { get; set; }

        [Required]
        public int SeriesId { get; set; }
    }
}