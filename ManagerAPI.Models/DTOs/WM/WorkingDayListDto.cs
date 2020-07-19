using System;
using System.Collections.Generic;

namespace ManagerAPI.Models.DTOs.WM
{
    public class WorkingDayListDto
    {
        public int Id { get; set; }
        public DateTime Day { get; set; }
        public int StartHour { get; set; }
        public int StartMin { get; set; }
        public int EndHour { get; set; }
        public int EndMin { get; set; }
        public int Type { get; set; }
        public List<WorkingFieldListDto> WorkingFields { get; set; }
    }
}
