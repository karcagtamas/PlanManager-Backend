using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Shared.Models
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