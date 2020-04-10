using System.ComponentModel.DataAnnotations;

namespace EventManager.Frontend.Data.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "User name is required")]
        [MaxLength(100, ErrorMessage = "User name's max length is 100")]
        [MinLength(6, ErrorMessage = "User name's min length is 6")]
        public string UserName { get; set; }
        
        [Required(ErrorMessage = "Password is required")]
        [MaxLength(100, ErrorMessage = "Password's max length is 100")]
        [MinLength(8, ErrorMessage = "Password's min length is 8")]
        public string Password { get; set; }
    }
}