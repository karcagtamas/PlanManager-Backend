namespace ManagerAPI.Shared.DTOs.SL
{
    /// <summary>
    /// My book DTO
    /// </summary>
    public class MyBookDto : BookDto
    {
        public bool IsMine { get; set; }
        public bool IsRead { get; set; }
    }
}