using System;
using System.Collections.Generic;
using System.Text;

namespace ManagerAPI.Shared.DTOs.WM
{
    public class WorkingDayStatDto
    {
        public int SumMinutes { get; set; }
        public bool IsEnough { get; set; }
        public bool IsOptimal { get; set; }
        public bool IsALot { get; set; }
        public bool IsActive { get; set; }
    }
}
