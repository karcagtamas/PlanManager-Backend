namespace ManagerAPI.Shared.DTOs.SL
{
    /// <summary>
    /// My movie selector list DTO
    /// </summary>
    public class MyMovieSelectorListDto : MovieListDto
    {
        public bool IsMine { get; set; }
        public bool IsSeen { get; set; }
    }
}