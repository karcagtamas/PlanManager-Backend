using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManagerAPI.Models.Entities

{
    public class ToDo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Title { get; set; }

        [Required]
        public bool IsSolved { get; set; } = false;

        [Required]
        public string OwnerId { get; set; }

        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime DueDate { get; set; }

        public virtual User Owner { get; set; }
    }
}
