namespace ManagerAPI.Shared.DTOs.WM
{
    /// <summary>
    /// Working field list DTO
    /// </summary>
    public class WorkingFieldListDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Length { get; set; }
    }
}