namespace ManagerAPI.Shared.Models.WM
{
    /// <summary>
    /// Working day type create or update model
    /// </summary>
    public class WorkingDayTypeModel
    {
        public string Title { get; set; }
        public bool DayIsActive { get; set; }
    }
}