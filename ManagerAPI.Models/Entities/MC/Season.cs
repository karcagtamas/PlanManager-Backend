using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Models.Entities.MC
{
    public class Season
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int Number { get; set; }

        [Required]
        public int SeriesId { get; set; }

        public virtual Series Series { get; set; }

        public virtual ICollection<Episode> Episodes { get; set; }
    }
}
