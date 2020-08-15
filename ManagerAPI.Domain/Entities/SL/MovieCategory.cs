using System.Collections.Generic;

namespace ManagerAPI.Domain.Entities.SL
{
    public class MovieCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<MovieMovieCategory> ConnectedMovies { get; set; }
    }
}