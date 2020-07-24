using System;
using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Shared.Models.WM
{
    public class WorkingDayModel
    {
        [Required]
        public DateTime Date { get; set; }
        
        [Required(ErrorMessage = "Field is required")]
        public int Type { get; set; }
    }
}
