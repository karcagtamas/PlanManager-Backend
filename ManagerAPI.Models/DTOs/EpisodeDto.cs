using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Models.DTOs
{
    public class EpisodeDto
    {
        public int Id { get; set; }

        [Required]
        public int Number { get; set; }

        public string Description { get; set; }
    }
}
