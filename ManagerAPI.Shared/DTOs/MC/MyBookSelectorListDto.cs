namespace ManagerAPI.Shared.DTOs.MC
{
    public class MyBookSelectorListDto : BookListDto
    {
        public bool IsMine { get; set; }
        public bool IsRead { get; set; }
    }
}