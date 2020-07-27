namespace ManagerAPI.Shared.DTOs.WM
{
    public class WorkingMonthStatDto
    {
        public decimal HourSum { get; set; }
        public double HourAvg { get; set; }
        public WorkingFieldListDto Fields { get; set; }
    }
}