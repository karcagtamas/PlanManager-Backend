using System.ComponentModel.DataAnnotations;
using ManagerAPI.Shared.Annotations;
using ManagerAPI.Shared.DTOs.SL;

namespace ManagerAPI.Shared.Models.SL
{
    /// <summary>
    /// Movie create or update model
    /// </summary>
    public class MovieModel
    {
        [Required(ErrorMessage = "Field is required")]
        [MaxLength(150, ErrorMessage = "Max length is 150")]
        public string Title { get; set; }

        [MaxLength(999, ErrorMessage = "Max length is 999")]
        public string Description { get; set; }

        [MinNumber(1900)] public int? ReleaseYear { get; set; }

        [MaxNumber(999)] [MinNumber(1)] public int? Length { get; set; }

        [MaxLength(60)] public string Director { get; set; }
        [MaxLength(200)] public string TrailerUrl { get; set; }

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
            this.ReleaseYear = movie.ReleaseYear;
            this.Length = movie.Length;
            this.Director = movie.Director;
            this.TrailerUrl = movie.TrailerUrl;
        }
    }
}