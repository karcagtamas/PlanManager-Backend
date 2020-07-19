using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EventManager.Client.Models.WM
{
    public class WorkingDayModel
    {
        [Required(ErrorMessage = "Field is required")]
        public int StartHour { get; set; }

        [Required(ErrorMessage = "Field is required")]
        public int StartMin { get; set; }

        [Required(ErrorMessage = "Field is required")]
        public int EndHour { get; set; }

        [Required(ErrorMessage = "Field is required")]
        public int EndMin { get; set; }

        [Required(ErrorMessage = "Field is required")]
        public int Type { get; set; }
    }
}
