namespace ManagerAPI.Shared.DTOs.MC
{
    public class MyBookDto : BookDto
    {
        public bool IsMine { get; set; }
        public bool IsRead { get; set; }
    }
}