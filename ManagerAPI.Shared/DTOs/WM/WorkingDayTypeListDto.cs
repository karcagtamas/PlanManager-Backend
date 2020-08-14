namespace ManagerAPI.Shared.DTOs.WM
{
    /// <summary>
    /// Working day type list DTO
    /// </summary>
    public class WorkingDayTypeListDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool DayIsActive { get; set; }
    }
}