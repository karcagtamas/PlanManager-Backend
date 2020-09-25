namespace ManagerAPI.Shared.DTOs.SL
{
    /// <summary>
    /// My book list DTO
    /// </summary>
    public class MyBookListDto : BookListDto
    {
        public bool Read { get; set; }
    }
}