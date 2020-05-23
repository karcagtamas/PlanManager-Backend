using System;
using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Models.DTOs
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
