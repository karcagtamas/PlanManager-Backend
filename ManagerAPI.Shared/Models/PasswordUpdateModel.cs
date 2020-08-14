using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Shared.Models
{
    /// <summary>
    /// Password update model
    /// </summary>
    public class PasswordUpdateModel
    {
        [Required(ErrorMessage = "Password is required")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "New password is required")]
        [MaxLength(100, ErrorMessage = "Maximum length is 100")]
        [MinLength(8, ErrorMessage = "Minimum length is 8")]
        public string NewPassword { get; set; }
    }
}