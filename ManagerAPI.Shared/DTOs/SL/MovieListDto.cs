namespace ManagerAPI.Shared.DTOs.SL
{
    /// <summary>
    /// Movie list DTO
    /// </summary>
    public class MovieListDto : IIdentified
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public string Creator { get; set; }
    }
}