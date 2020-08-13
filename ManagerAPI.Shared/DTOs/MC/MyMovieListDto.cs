namespace ManagerAPI.Shared.DTOs.MC
{
    /// <summary>
    /// My movie list DTO
    /// </summary>
    public class MyMovieListDto : MovieListDto
    {
        public bool Seen { get; set; }
    }
}