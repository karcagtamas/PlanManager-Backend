using System;
using System.Collections.Generic;

namespace ManagerAPI.Models.DTOs
{
    public class WorkingDayListDto
    {
        public int Id { get; set; }
        public DateTime Day { get; set; }
        public int StartHour { get; set; }
        public int StartMin { get; set; }
        public int EndHour { get; set; }
        public int EndMin { get; set; }
        public WorkingDayTypeDto Type { get; set; }
        public List<WorkingFieldListDto> WorkingFields { get; set; }
    }
}
