using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Models.Models.MC
{
    public class EpisodeModel
    {
        [Required]
        public int Number { get; set; }

        public string Description { get; set; }
    }
}