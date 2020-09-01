using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Shared.Models.SL
{
    /// <summary>
    /// Movie comment create or update model
    /// </summary>
    public class MovieCommentModel
    {
        [Required]
        public int MovieId { get; set; }

        [Required]
        [MaxLength(500)]
        public string Comment { get; set; }
    }
}