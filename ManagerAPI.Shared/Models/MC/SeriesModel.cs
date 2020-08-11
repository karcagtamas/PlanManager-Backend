using System.ComponentModel.DataAnnotations;
using ManagerAPI.Shared.DTOs.MC;

namespace ManagerAPI.Shared.Models.MC
{
    public class SeriesModel
    {
        [Required]
        [MaxLength(150)]
        public string Title { get; set; }

        public string Description { get; set; }

        public int? StartYear { get; set; }

        public int? EndYear { get; set; }

        public SeriesModel() {}

        public SeriesModel(SeriesDto series) {
            this.Title = series.Title;
            this.Description = series.Description;
            this.StartYear = series.StartYear;
            this.EndYear = series.EndYear;
        }

    }
}