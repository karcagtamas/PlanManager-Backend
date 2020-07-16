using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Models.Models.MC
{
    public class MovieModel
    {
        [Required]
        [MaxLength(150)]
        public string Title { get; set; }

        [MaxLength(999)]
        public string Description { get; set; }

        [Required]
        public int Year { get; set; }
    }
}