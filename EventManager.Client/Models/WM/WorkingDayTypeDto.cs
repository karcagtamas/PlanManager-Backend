using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManager.Client.Models.WM
{
    public class WorkingDayTypeDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool DayIsActive { get; set; }
    }
}
