using System;
using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Shared.Models.WM
{
    public class WorkingDayModel
    {
        [Required(ErrorMessage = "Field is required")]
        public int Type { get; set; }
    }
}
