using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Models.Models
{
    public class PostModel
    {
        [Required]
        public int? Id { get; set; }

        [Required]
        [MaxLength(512)]
        public string Content { get; set; }
    }
}