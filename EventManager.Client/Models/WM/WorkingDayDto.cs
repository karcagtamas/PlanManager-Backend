using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EventManager.Client.Models.WM
{
    public class WorkingDayDto
    {
        public int? Id { get; set; }

        [Required]
        public DateTime Day { get; set; }

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
