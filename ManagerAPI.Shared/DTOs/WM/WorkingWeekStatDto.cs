using System.Collections.Generic;

namespace ManagerAPI.Shared.DTOs.WM
{
    public class WorkingWeekStatDto
    {
        public int HourSum { get; set; }
        public double HourAvg { get; set; }
        public List<WorkingFieldListDto> Fields { get; set; }
    }
}