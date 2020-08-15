using System;
using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Domain.Entities.SL
{
    public class UserMovie
    {
        [Required]
        public int MovieId { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public bool Seen { get; set; }

        public DateTime? SeenOn { get; set; }

        [Required]
        public DateTime AddOn { get; set; }

        public virtual Movie Movie { get; set; }
        public virtual User User { get; set; }
    }
}
