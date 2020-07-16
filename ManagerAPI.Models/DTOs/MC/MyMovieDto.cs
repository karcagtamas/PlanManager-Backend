namespace ManagerAPI.Models.DTOs.MC
{
    public class MyMovieDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public string Description { get; set; }
        public bool Seen { get; set; }
    }
}
