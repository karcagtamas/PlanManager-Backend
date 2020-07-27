namespace ManagerAPI.Shared.DTOs.WM
{
    public class WorkingWeekStatDto
    {
        public int HourSum { get; set; }
        public double HourAvg { get; set; }
        public WorkingFieldListDto Fields { get; set; }
    }
}