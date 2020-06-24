using System.ComponentModel.DataAnnotations;

namespace EventManager.Client.Models.News {
    public class PostModel {
        [Required]
        [MaxLength (512)]
        public string Content { get; set; }
    }
}