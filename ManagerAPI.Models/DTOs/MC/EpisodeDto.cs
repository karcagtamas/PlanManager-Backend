using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Models.DTOs.MC
{
    public class EpisodeDto
    {
        public int Id { get; set; }

        [Required]
        public int Number { get; set; }

        public string Description { get; set; }
    }
}
