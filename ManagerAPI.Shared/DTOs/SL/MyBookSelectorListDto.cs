namespace ManagerAPI.Shared.DTOs.SL
{
    /// <summary>
    /// My book selector list DTO
    /// </summary>
    public class MyBookSelectorListDto : BookListDto
    {
        public bool IsMine { get; set; }
        public bool IsRead { get; set; }
    }
}