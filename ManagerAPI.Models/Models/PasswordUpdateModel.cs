using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Models.Models
{
    public class PasswordUpdateModel
    {
        [Required]
        public string OldPassword { get; set; }
        
        [Required]
        [MaxLength(100, ErrorMessage = "Password's max length is 100")]
        [MinLength(8)]
        public string NewPassword { get; set; }
    }
}