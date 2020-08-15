namespace ManagerAPI.Shared.DTOs.SL
{
    /// <summary>
    /// My movie DTO
    /// </summary>
    public class MyMovieDto : MovieDto
    {
        public bool IsMine { get; set; }
        public bool IsSeen { get; set; }
    }
}