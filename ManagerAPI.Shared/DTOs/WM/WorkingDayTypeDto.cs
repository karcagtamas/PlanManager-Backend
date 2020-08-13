namespace ManagerAPI.Shared.DTOs.WM
{
    /// <summary>
    /// Working day type DTO
    /// </summary>
    public class WorkingDayTypeDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool DayIsActive { get; set; }
    }
}