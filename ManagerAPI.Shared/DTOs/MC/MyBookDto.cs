namespace ManagerAPI.Shared.DTOs.MC
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