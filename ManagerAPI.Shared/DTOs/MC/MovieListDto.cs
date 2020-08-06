namespace ManagerAPI.Shared.DTOs.MC
{
    public class MovieListDto : IIdentified
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public string Creator { get; set; }
        public string Description { get; set; }
    }
}
