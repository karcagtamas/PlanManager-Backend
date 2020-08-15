namespace ManagerAPI.Shared.DTOs.SL
{
    /// <summary>
    /// My movie list DTO
    /// </summary>
    public class MyMovieListDto : MovieListDto
    {
        public bool IsSeen { get; set; }
    }
}