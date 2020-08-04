using System.ComponentModel.DataAnnotations;
using ManagerAPI.Shared.DTOs.MC;

namespace ManagerAPI.Shared.Models.MC
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

        public MovieModel() {}

        public MovieModel(MovieDto movie) {
            this.Title = movie.Title;
            this.Description = movie.Description;
            this.Year = movie.Year;
        }
    }
}