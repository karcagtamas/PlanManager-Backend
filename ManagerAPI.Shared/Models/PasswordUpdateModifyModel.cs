using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ManagerAPI.Shared.Models
{
    public class PasswordUpdateModifyModel
    {
        [Required(ErrorMessage = "Password is required")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "New password is required")]
        [MaxLength(100, ErrorMessage = "Maximum length is 100")]
        [MinLength(8, ErrorMessage = "Minimum length is 8")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Confirmation is required")]
        [Compare("NewPassword", ErrorMessage = "New password confirmation is not equal with the new password")]
        public string ConfirmNewPassword { get; set; }
    }
}
