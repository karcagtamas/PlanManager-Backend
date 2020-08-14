using System.Collections.Generic;

namespace ManagerAPI.Shared.DTOs.WM
{
    /// <summary>
    /// Working month stat DTO
    /// </summary>
    public class WorkingMonthStatDto
    {
        public decimal HourSum { get; set; }
        public double HourAvg { get; set; }
        public int ActiveDays { get; set; }
        public int WorkDays { get; set; }
        public List<WorkingDayTypeCountDto> Counts { get; set; }
        public List<WorkingFieldListDto> Fields { get; set; }
    }
}