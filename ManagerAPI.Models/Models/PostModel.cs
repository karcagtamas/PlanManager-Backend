using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Models.Models
{
    public class PostModel
    {
        [Required]
        [MaxLength(512)]
        public string Content { get; set; }
    }
}