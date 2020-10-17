using ManagerAPI.Shared.Annotations;
using ManagerAPI.Shared.DTOs.SL;
using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Shared.Models.SL
{
    /// <summary>
    /// Series create or update model
    /// </summary>
    public class SeriesModel
    {
        [Required(ErrorMessage = "Field is required")]
        [MaxLength(150, ErrorMessage = "Max length is 150")]
        public string Title { get; set; }

        [MaxLength(999, ErrorMessage = "Max length is 999")]
        public string Description { get; set; }
        [MinNumber(1900)] public int? StartYear { get; set; }
        [MinNumber(1900)] public int? EndYear { get; set; }

        [MaxLength(200, ErrorMessage = "Max length is 200")]
        public string TrailerUrl { get; set; }

        /// <summary>
        /// Empty init
        /// </summary>
        public SeriesModel()
        {
        }

        /// <summary>
        /// Model from series data object
        /// </summary>
        /// <param name="series">Series data object</param>
        public SeriesModel(SeriesDto series)
        {
            Title = series.Title;
            Description = series.Description;
            StartYear = series.StartYear;
            EndYear = series.EndYear;
            TrailerUrl = series.TrailerUrl;
        }
    }
}