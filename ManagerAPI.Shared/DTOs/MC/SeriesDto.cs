using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Shared.DTOs.MC
{
    public class SeriesDto
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public int? StartYear { get; set; }

        public int? EndYear { get; set; }

    }
}
