using System;
using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Models.Models.WM
{
    public class WorkingDayModel
    {
        [Required]
        public int StartHour { get; set; }

        [Required]
        public int StartMin { get; set; }

        [Required]
        public int EndHour { get; set; }

        [Required]
        public int EndMin { get; set; }

        [Required]
        public int Type { get; set; }
    }
}
