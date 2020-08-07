namespace ManagerAPI.Shared.DTOs.MC
{
    public class MyMovieSelectorListDto : MovieListDto
    {
        public bool IsMine { get; set; }
        public bool IsSeen { get; set; }
    }
}