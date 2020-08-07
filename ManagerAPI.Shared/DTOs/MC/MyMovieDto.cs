namespace ManagerAPI.Shared.DTOs.MC
{
    public class MyMovieDto : MovieDto
    {
        public bool IsMine { get; set; }
        public bool IsSeen { get; set; }
    }
}