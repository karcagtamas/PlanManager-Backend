using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Shared.Models
{
    public class PostModel
    {
        [Required]
        [MaxLength(512)]
        public string Content { get; set; }
    }
}