using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Models.DTOs
{
    public class MovieUpdateDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Title { get; set; }

        [MaxLength(999)]
        public string Description { get; set; }

        [Required]
        public int Year { get; set; }
    }
}
