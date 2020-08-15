using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Domain.Entities.SL
{
    public class MovieMovieCategory
    {
        [Required] public int MovieId { get; set; }

        [Required] public int CategoryId { get; set; }
        public virtual Movie Movie { get; set; }
        public virtual MovieCategory Category { get; set; }
    }
}