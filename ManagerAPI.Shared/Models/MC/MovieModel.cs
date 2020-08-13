using System.ComponentModel.DataAnnotations;
using ManagerAPI.Shared.DTOs.MC;

namespace ManagerAPI.Shared.Models.MC
{
    public class MovieModel
    {
        [Required(ErrorMessage = "Field is required")]
        [MaxLength(150, ErrorMessage = "Max length is 150")]
        public string Title { get; set; }

        [MaxLength(999, ErrorMessage = "Max length is 999")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Field is required")]
        public int Year { get; set; }

        /// <summary>
        /// Empty init
        /// </summary>
        public MovieModel()
        {
        }

        /// <summary>
        /// Model from movie data object
        /// </summary>
        /// <param name="movie">Movie data object</param>
        public MovieModel(MovieDto movie)
        {
            this.Title = movie.Title;
            this.Description = movie.Description;
            this.Year = movie.Year;
        }
    }
}