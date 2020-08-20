using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Domain.Entities.SL
{
    public class SeriesSeriesCategory
    {
        [Required] public int SeriesId { get; set; }

        [Required] public int CategoryId { get; set; }
        public virtual Series Series { get; set; }
        public virtual SeriesCategory Category { get; set; }
    }
}