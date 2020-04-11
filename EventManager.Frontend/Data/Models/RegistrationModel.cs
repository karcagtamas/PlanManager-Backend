using System.ComponentModel.DataAnnotations;

namespace EventManager.Frontend.Data.Models
{
    public class RegistrationModel
    {
        [Required(ErrorMessage = "User name is required")]
        [MinLength(6, ErrorMessage = "User name's min length is 6")]
        [MaxLength(100, ErrorMessage = "User name's max length is 100")]
        public string UserName { get; set; }
        
        [Required(ErrorMessage = "Password is required")]
        [MinLength(8, ErrorMessage = "Password's min length is 8")]
        [MaxLength(100, ErrorMessage = "Password's max length is 100")]
        public string Password { get; set; }
        
        [Required(ErrorMessage = "Password confirm is required")]
        public string PasswordConfirm { get; set; }
        
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Email is not valid")]
        public string Email { get; set; }
        
        [MaxLength(120, ErrorMessage = "Full name's max length is 120")]
        public string FullName { get; set; }
    }
}