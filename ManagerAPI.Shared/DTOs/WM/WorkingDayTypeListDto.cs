using System;
using System.Collections.Generic;
using System.Text;

namespace ManagerAPI.Shared.DTOs.WM
{
    public class WorkingDayTypeListDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool DayIsActive { get; set; }
    }
}
