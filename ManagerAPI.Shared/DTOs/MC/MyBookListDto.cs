namespace ManagerAPI.Shared.DTOs.MC
{
    /// <summary>
    /// My book list DTO
    /// </summary>
    public class MyBookListDto : BookListDto
    {
        public bool Read { get; set; }
    }
}