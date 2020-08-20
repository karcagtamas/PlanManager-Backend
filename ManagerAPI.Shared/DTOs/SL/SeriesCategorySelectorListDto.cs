namespace ManagerAPI.Shared.DTOs.SL
{
    /// <summary>
    /// Series category selector list data object
    /// </summary>
    public class SeriesCategorySelectorListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsSelected { get; set; }
    }
}