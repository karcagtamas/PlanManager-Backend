using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManagerAPI.Domain.Entities

{
    public class Task
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set;}

        [Required]
        public bool IsSolved { get; set; } = false;

        [Required]
        public string OwnerId { get; set; }

        [Required]
        public DateTime Creation { get; set; }

        [Required]
        public DateTime LastUpdate { get; set; }

        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime Deadline { get; set; }

        public virtual User Owner { get; set; }
    }
}
