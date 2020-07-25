using System.ComponentModel.DataAnnotations;

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

    }
}