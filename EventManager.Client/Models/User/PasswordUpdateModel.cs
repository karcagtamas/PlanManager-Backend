using System.ComponentModel.DataAnnotations;

namespace EventManager.Client.Models.User
{
    public class PasswordUpdateModel
    {
        [Required]
        public string OldPassword { get; set; }
        
        [Required]
        [MaxLength(100)]
        [MinLength(8)]
        public string NewPassword { get; set; }
    }
}