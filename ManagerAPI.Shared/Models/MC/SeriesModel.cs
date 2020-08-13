using System.ComponentModel.DataAnnotations;
using ManagerAPI.Shared.DTOs.MC;

namespace ManagerAPI.Shared.Models.MC
{
    /// <summary>
    /// Series create or update model
    /// </summary>
    public class SeriesModel
    {
        [Required(ErrorMessage = "Field is required")]
        [MaxLength(150, ErrorMessage = "Max length is 150")]
        public string Title { get; set; }

        public string Description { get; set; }

        public int? StartYear { get; set; }

        public int? EndYear { get; set; }

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
            this.Title = series.Title;
            this.Description = series.Description;
            this.StartYear = series.StartYear;
            this.EndYear = series.EndYear;
        }
    }
}