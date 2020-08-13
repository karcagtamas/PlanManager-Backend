using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Shared.Models
{
    /// <summary>
    /// News create or update model
    /// </summary>
    public class PostModel
    {
        [Required(ErrorMessage = "Field is required")]
        [MaxLength(512, ErrorMessage = "Max length is 512")]
        public string Content { get; set; }
    }
}