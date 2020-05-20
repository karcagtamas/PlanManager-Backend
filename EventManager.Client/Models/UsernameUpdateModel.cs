using System.ComponentModel.DataAnnotations;

namespace EventManager.Client.Models
{
    public class UsernameUpdateModel
    {
        [Required(ErrorMessage = "Username is required")]
        [MaxLength(100, ErrorMessage = "Maximum length is 100")]
        [MinLength(6, ErrorMessage = "Minimum length is 6")]
        public string UserName { get; set; }
    }
}