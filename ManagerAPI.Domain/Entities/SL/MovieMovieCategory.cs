namespace ManagerAPI.Domain.Entities.SL
{
    public class MovieMovieCategory
    {
        public int MovieId { get; set; }
        public int CategoryId { get; set; }
        public virtual Movie Movie { get; set; }
        public virtual MovieCategory Category { get; set; }
    }
}