using System;
using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Domain.Entities.MC
{
    public class UserBook
    {
        [Required]
        public int BookId { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public bool Read { get; set; }

        public DateTime? ReadOn { get; set; }

        [Required]
        public DateTime AddOn { get; set; }

        public virtual Book Book { get; set; }
        public virtual User User { get; set; }
    }
}