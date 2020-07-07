using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Models.Entities.MC
{
    public class Episode
    {
        public int Id { get; set; }

        [Required]
        public int Number { get; set; }

        public string Description { get; set; }

        [Required]
        public int SeasonId { get; set; }

        public virtual Season Season { get; set; }

        public virtual ICollection<UserEpisode> UserEpisodes { get; set; }
    }
}
