namespace ManagerAPI.Shared.DTOs.MC
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