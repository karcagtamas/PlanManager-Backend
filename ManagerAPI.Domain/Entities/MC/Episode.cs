using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Domain.Entities.MC
{
    public class Episode
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int Number { get; set; }

        public string Description { get; set; }

        [Required]
        public int SeasonId { get; set; }

        public virtual Season Season { get; set; }

        public virtual ICollection<UserEpisode> ConnectedUsers { get; set; }
    }
}
