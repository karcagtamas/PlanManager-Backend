using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManager.Client.Models.WM
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
