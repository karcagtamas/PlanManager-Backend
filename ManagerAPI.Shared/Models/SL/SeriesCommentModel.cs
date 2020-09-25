using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Shared.Models.SL
{
    /// <summary>
    /// Series comment create or update model
    /// </summary>
    public class SeriesCommentModel
    {
        [Required]
        public int SeriesId { get; set; }

        [Required]
        [MaxLength(500)]
        public string Comment { get; set; }
    }
}