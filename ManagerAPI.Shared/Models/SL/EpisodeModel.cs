using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Shared.Models.SL
{
    /// <summary>
    /// Episode create or update model
    /// </summary>
    public class EpisodeModel
    {
        [Required(ErrorMessage = "Field is required")]
        public int Number { get; set; }

        [MaxLength(300, ErrorMessage = "Max length is 300")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Field is required")]
        [MaxLength(150, ErrorMessage = "Max length is 150")]
        public string Title { get; set; }

        [Required] public int SeasonId { get; set; }
    }
}