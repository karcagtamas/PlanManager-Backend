using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Shared.Models.MC
{
    public class EpisodeModel
    {
        [Required]
        public int Number { get; set; }

        public string Description { get; set; }

        [Required]
        public int SeasonId { get; set; }
    }
}