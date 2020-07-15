using System;
using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Models.Entities.MC
{
    public class UserEpisode
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public int EpisodeId { get; set; }

        [Required]
        public bool Seen { get; set; }

        public DateTime? SeenOn { get; set; }

        public virtual User User { get; set; }

        public virtual Episode Episode { get; set; }
    }
}
